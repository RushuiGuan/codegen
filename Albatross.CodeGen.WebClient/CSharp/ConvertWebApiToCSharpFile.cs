using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeAnalysis.Syntax;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.CSharp;
using Microsoft.CodeAnalysis;
using System.Linq;
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using Albatross.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Albatross.CodeGen.WebClient.CSharp {
	public class ConvertWebApiToCSharpFile : IConvertObject<ControllerInfo, FileDeclaration> {
		const string ProxyService = "ProxyService";
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertWebApiToCSharpFile(Compilation compilation, CodeGenSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.compilation = compilation;
			this.settings = settings;
			this.typeConverter = typeConverter;
		}

		ITypeExpression GetMethodReturnType(ITypeSymbol typeSymbol) {
			if (typeSymbol.SpecialType == SpecialType.System_Void) {
				return Defined.Types.Task;
			} else {
				return new TypeExpression("Task") {
					GenericArguments = [typeConverter.Convert(typeSymbol),]
				};
			}
		}
		ListOfParameterDeclarations GetMethodParameters(MethodInfo method) {
			return new ListOfParameterDeclarations(method.Parameters.Select(param => new ParameterDeclaration {
				Name = new IdentifierNameExpression(param.Name),
				Type = typeConverter.Convert(param.Type),
			}));
		}

		InterfaceDeclaration CreateInterface(ControllerInfo controller) {
			var interfaceName = GetInterfaceName(controller);
			return new InterfaceDeclaration(interfaceName) {
				IsPartial = true,
				Attributes = new List<AttributeExpression>().AddIf(controller.IsObsolete, Defined.Attributes.Obsolete),
				Methods = from method in controller.Methods select new MethodDeclaration {
					Name = new IdentifierNameExpression(method.Name),
					ReturnType = GetMethodReturnType(method.ReturnType),
					Parameters = GetMethodParameters(method)
				}
			};
		}
		ClassDeclaration CreateClass(ControllerInfo controller) {
			return new ClassDeclaration {
				Name = new IdentifierNameExpression(GetClassName(controller)),
				IsPartial = true,
				AccessModifier = settings.CSharpWebClientSettings.UseInternalProxy ? Defined.Keywords.Internal : Defined.Keywords.Public,
				BaseTypes = new List<ITypeExpression> { new TypeExpression("ClientBase") }.AddIf(settings.CSharpWebClientSettings.UseInterface, new TypeExpression(GetInterfaceName(controller))),
				Attributes = new List<AttributeExpression>().AddIf(controller.IsObsolete, Defined.Attributes.Obsolete),
				Constructors = CreateConstructors(controller).ToList(),
				Fields = [
					new FieldDeclaration{
						AccessModifier = Defined.Keywords.Public,
						IsConst = true,
						Type = new TypeExpression("string"),
						Name = new IdentifierNameExpression("ControllerPath"),
						Initializer = new StringLiteralExpression(controller.Route),
					},
				],
				Methods = from method in controller.Methods select CreateMethod(method),
			};
		}
		string GetInterfaceName(ControllerInfo controller) {
			return $"I{controller.ControllerName}{ProxyService}";
		}
		string GetClassName(ControllerInfo controller) {
			return $"{controller.ControllerName}{ProxyService}";
		}
		MethodDeclaration CreateMethod(MethodInfo method) {
			return new MethodDeclaration {
				Name = new IdentifierNameExpression(method.Name),
				ReturnType = GetMethodReturnType(method.ReturnType),
				Parameters = GetMethodParameters(method),
				Body = new CompositeExpression(
					BuildPath(method).AsEnumerable().Concat(BuildQuery(method))
				)
			};
		}
		IExpression BuildPath(MethodInfo method) {
			return new VariableDeclaration {
				Type = Defined.Types.String,
				Identifier = new IdentifierNameExpression("path"),
				Assignment = new StringInterpolationExpression {
					Expressions = BuildRouteSegments(method.RouteSegments).ToArray(),
				}
			};
		}
		IEnumerable<IExpression> BuildQuery(MethodInfo method) {
			yield return new VariableDeclaration {
				Type = Defined.Types.Var,
				Identifier = new IdentifierNameExpression("queryString"),
				Assignment = new NewObjectExpression {
					CallableExpression = new IdentifierNameExpression("NameValueCollection"),
				}
			};
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				if (param.Type.TryGetCollectionElementType(compilation, out var elementType)) {
					yield return new ForeachExpression {
						IterationVariable = new VariableDeclaration {
							Identifier = new IdentifierNameExpression("item"),
						},
						Collection = new IdentifierNameExpression(param.Name),
						Body = CreateAddQueryStringStatement(method.Settings, elementType, param.QueryKey, "item")
					};
				} else {
					CreateAddQueryStringStatement(method.Settings, param.Type, param.QueryKey, param.Name);
				}
			}
		}

		IEnumerable<ConstructorDeclaration> CreateConstructors(ControllerInfo from) {
			settings.CSharpWebClientSettings.ConstructorSettings.TryGetValue(from.Controller.Name, out var constructorSettings);
			if (constructorSettings == null) {
				settings.CSharpWebClientSettings.ConstructorSettings.TryGetValue("*", out constructorSettings);
			}
			if (constructorSettings?.Omit != true) {
				var constructor = new ConstructorDeclaration {
					Name = new IdentifierNameExpression(GetClassName(from)),
					Parameters = [
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("logger"),
							Type = new TypeExpression("ILogger", GetClassName(from)),
						},
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("client"),
							Type = new TypeExpression("HttpClient"),
						},
					],
					BaseConstructorInvocation = new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("base"),
						Arguments = new ListOfArguments(new List<IExpression> {
							new IdentifierNameExpression("logger"),
							new IdentifierNameExpression("client"),
						}.AddIfNotNull(string.IsNullOrEmpty(constructorSettings?.CustomJsonSettings) ? null : new IdentifierNameExpression(constructorSettings!.CustomJsonSettings)))
					},
				};
				return [constructor];
			} else {
				return [];
			}
		}

		ListOfArguments CreateRequestArguments() {
		}

		IEnumerable<IExpression> BuildRequest() {
			yield return new UsingExpression {
				Resource = new VariableDeclaration {
					Identifier = new IdentifierNameExpression("request"),
					Type = Defined.Types.Var,
					Assignment = new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("this.CreateRequest"),
						Arguments = CreateRequestArguments()
					},
				},
				Body = new CompositeExpression {
					// Method body goes here
				}
			}
			using (codeStack.NewScope(new UsingStatementBuilder())) {
				using (codeStack.NewScope(new VariableBuilder("request"))) {
					codeStack.With(new ThisExpression());
					var fromBody = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
					if (fromBody == null) {
						codeStack.With(new IdentifierNode("CreateRequest"));
					} else if (fromBody.Type.SpecialType == SpecialType.System_String) {
						codeStack.With(new IdentifierNode("CreateStringRequest"));
					} else {
						codeStack.With(new GenericIdentifierNode("CreateJsonRequest", fromBody.Type.AsTypeNode()));
					}
					using (codeStack.ToNewScope(new InvocationExpressionBuilder())) {
						using (codeStack.NewScope(new ArgumentListBuilder())) {
							codeStack.With(new IdentifierNode("HttpMethod"))
								.With(new IdentifierNode(method.HttpMethod))
								.To(new MemberAccessBuilder());
							codeStack.With(new IdentifierNode("path"));
							codeStack.With(new IdentifierNode("queryString"));
							if (fromBody != null) {
								codeStack.With(new IdentifierNode(fromBody.Name));
							}
						}
					}
				}
				if (method.ReturnType.SpecialType == SpecialType.System_Void) {
					using (codeStack.NewScope()) {
						codeStack.With(new ThisExpression()).ToNewBegin(new InvocationExpressionBuilder("GetRawResponse").Await())
							.Begin(new ArgumentListBuilder())
							.With(new IdentifierNode("request"))
							.End()
							.End();
					}
				} else {
					using (codeStack.NewScope(new ReturnExpressionBuilder())) {
						if (method.ReturnType.SpecialType == SpecialType.System_String) {
							codeStack.With(new ThisExpression()).ToNewBegin(new InvocationExpressionBuilder("GetRawResponse").Await())
								.Begin(new ArgumentListBuilder())
								.With(new IdentifierNode("request"))
								.End()
								.End();
						} else {
							string functionName;
							if (method.ReturnType.IsNullable(compilation)) {
								functionName = "GetJsonResponse";
							} else if (method.ReturnType.IsValueType) {
								functionName = "GetRequiredJsonResponseForValueType";
							} else {
								functionName = "GetRequiredJsonResponse";
							}
							codeStack.With(new ThisExpression())
								.With(new GenericIdentifierNode(functionName, method.ReturnType.AsTypeNode()))
								.ToNewBegin(new InvocationExpressionBuilder().Await())
								.Begin(new ArgumentListBuilder())
								.With(new IdentifierNode("request"))
								.End()
								.End();
						}
					}
				}
			}
		}

		public FileDeclaration Convert(ControllerInfo from) {
			var proxyClassName = from.ControllerName + ProxyService;
			var fileName = $"{proxyClassName}.generated";
			var file = new FileDeclaration(fileName) {
				Imports = [
					new ImportExpression("Albatross.Dates"),
					new ImportExpression("System.Net.Http"),
					new ImportExpression("System.Threading.Tasks"),
					new ImportExpression("Microsoft.Extensions.Logging"),
					new ImportExpression("Albatross.WebClient"),
					new ImportExpression("System.Collections.Specialized"),
				],
				Interfaces = settings.CSharpWebClientSettings.UseInterface ? [CreateInterface(from)] : [],
				Classes = [CreateClass(from)],
			};
			var classDeclaration = new ClassDeclaration(proxyClassName) {
				IsPartial = true,
				AccessModifier = settings.CSharpWebClientSettings.UseInternalProxy ? Defined.Keywords.Internal : Defined.Keywords.Public,
				BaseTypes = [new TypeExpression("ClientBase")],
				Constructors = CreateConstructors(from).ToList(),
			};

			var codeStack = new CodeStack();
			using (codeStack.NewScope(new CompilationUnitBuilder())) {
				using (codeStack.NewScope(new NamespaceDeclarationBuilder(settings.CSharpWebClientSettings.Namespace))) {
					using (codeStack.NewScope(classBuilder.Partial())) {
						foreach (var method in from.Methods) {
							TypeNode returnType;
							if (method.ReturnType.SpecialType == SpecialType.System_Void) {
								returnType = new TypeNode("Task");
							} else {
								returnType = new GenericIdentifierNode("Task", method.ReturnType.AsTypeNode());
							}
							using (codeStack.NewScope(new MethodDeclarationBuilder(returnType, method.Name).Public().Async())) {
								if (method.IsObsolete) {
									codeStack.Complete(new AttributeBuilder("System.ObsoleteAttribute"));
								}
								foreach (var param in method.Parameters) {
									codeStack.With(new ParameterNode(param.Type.AsTypeNode(), param.Name));
								}
								using (codeStack.NewScope(new VariableBuilder("string", "path"))) {
									using (codeStack.NewScope(new InterpolatedStringBuilder())) {
										codeStack.With(new IdentifierNode("ControllerPath"));
										if (method.RouteSegments.Any()) {
											codeStack.With(new LiteralNode(@"/"));
										}
										foreach (var routeSegment in method.RouteSegments) {
											BuildRouteSegment(codeStack, method, routeSegment);
										}
									}
								}
								codeStack.Begin(new VariableBuilder("var", "queryString")).Complete(new NewObjectBuilder("NameValueCollection")).End();
								foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
									using (codeStack.NewScope()) {
										if (param.Type.TryGetCollectionElementType(compilation, out var elementType)) {
											using (codeStack.NewScope(new ForEachStatementBuilder(null, "item", param.Name))) {
												CreateAddQueryStringStatement(codeStack, method.Settings, elementType!, param.QueryKey, "item");
											}
										} else {
											CreateAddQueryStringStatement(codeStack, method.Settings, param.Type, param.QueryKey, param.Name);
										}
									}
								}
								using (codeStack.NewScope(new UsingStatementBuilder())) {
									using (codeStack.NewScope(new VariableBuilder("request"))) {
										codeStack.With(new ThisExpression());
										var fromBody = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
										if (fromBody == null) {
											codeStack.With(new IdentifierNode("CreateRequest"));
										} else if (fromBody.Type.SpecialType == SpecialType.System_String) {
											codeStack.With(new IdentifierNode("CreateStringRequest"));
										} else {
											codeStack.With(new GenericIdentifierNode("CreateJsonRequest", fromBody.Type.AsTypeNode()));
										}
										using (codeStack.ToNewScope(new InvocationExpressionBuilder())) {
											using (codeStack.NewScope(new ArgumentListBuilder())) {
												codeStack.With(new IdentifierNode("HttpMethod"))
													.With(new IdentifierNode(method.HttpMethod))
													.To(new MemberAccessBuilder());
												codeStack.With(new IdentifierNode("path"));
												codeStack.With(new IdentifierNode("queryString"));
												if (fromBody != null) {
													codeStack.With(new IdentifierNode(fromBody.Name));
												}
											}
										}
									}
									if (method.ReturnType.SpecialType == SpecialType.System_Void) {
										using (codeStack.NewScope()) {
											codeStack.With(new ThisExpression()).ToNewBegin(new InvocationExpressionBuilder("GetRawResponse").Await())
												.Begin(new ArgumentListBuilder())
												.With(new IdentifierNode("request"))
												.End()
												.End();
										}
									} else {
										using (codeStack.NewScope(new ReturnExpressionBuilder())) {
											if (method.ReturnType.SpecialType == SpecialType.System_String) {
												codeStack.With(new ThisExpression()).ToNewBegin(new InvocationExpressionBuilder("GetRawResponse").Await())
													.Begin(new ArgumentListBuilder())
													.With(new IdentifierNode("request"))
													.End()
													.End();
											} else {
												string functionName;
												if (method.ReturnType.IsNullable(compilation)) {
													functionName = "GetJsonResponse";
												} else if (method.ReturnType.IsValueType) {
													functionName = "GetRequiredJsonResponseForValueType";
												} else {
													functionName = "GetRequiredJsonResponse";
												}
												codeStack.With(new ThisExpression())
													.With(new GenericIdentifierNode(functionName, method.ReturnType.AsTypeNode()))
													.ToNewBegin(new InvocationExpressionBuilder().Await())
													.Begin(new ArgumentListBuilder())
													.With(new IdentifierNode("request"))
													.End()
													.End();
											}
										}
									}
								}
							}
						}
					}
					return codeStack;
				}
			}
		}

		IEnumerable<IExpression> BuildRouteSegments(IEnumerable<IRouteSegment> segments) {
			yield return new IdentifierNameExpression("ControllerPath");
			foreach (var segment in segments) {
				if (segment is RouteParameterSegment routeParam) {
					var type = routeParam.ParameterInfo?.Type;
					if (type.Is(compilation.DateTime()) || type.Is(compilation.DateTimeOffset()) || type.Is(compilation.TimeOnly()) || type.Is(compilation.DateOnly())) {
						yield return new InvocationExpression {
							CallableExpression = new IdentifierNameExpression("ISO8601String"),
							Arguments = new ListOfArguments(new IdentifierNameExpression(routeParam.Text))
						};
					} else {
						yield return new IdentifierNameExpression(routeParam.Text);
					}
				} else {
					yield return new StringLiteralExpression(segment.Text);
				}
			}
		}
		bool IsDate(ITypeSymbol type) => type.Is(compilation.DateTime()) 
			|| type.Is(compilation.DateTimeOffset()) 
			|| type.Is(compilation.TimeOnly()) 
			|| type.Is(compilation.DateOnly());

		IExpression GetQueryStringValue(ITypeSymbol type, string variableName) {
			if (type.SpecialType == SpecialType.System_String) {
				return new IdentifierNameExpression(variableName);
			} else if (IsDate(type)) {
				return new InvocationExpression {
					CallableExpression = new IdentifierNameExpression($"{variableName}.ISO8601String"),
				};
			} else {
				return new StringInterpolationExpression {
					Expressions = [new IdentifierNameExpression(variableName)]
				};
			}
		}

		IExpression CreateAddQueryStringStatement(WebClientMethodSettings settings, ITypeSymbol type, string queryKey, string variableName) {
			return new InvocationExpression {
				CallableExpression = new IdentifierNameExpression("queryString.Add"),
				Arguments = new ListOfArguments(GetQueryStringValue(type, variableName))
			};
		}
		CodeStack CreateAddQueryStringStatement(CodeStack codeStack, WebClientMethodSettings settings, ITypeSymbol type, string queryKey, string variableName) {
			ITypeSymbol finalType = type;
			if (type.IsNullable(compilation)) {
				codeStack.Begin(new IfStatementBuilder());
				codeStack.With(new NotEqualStatementNode(new IdentifierNode(variableName), new NullExpressionNode()));

				if (type.TryGetNullableValueType(compilation, out var valueType)) {
					finalType = valueType!;
				}
			}

			using (codeStack.NewScope()) {
				using (codeStack.With(new IdentifierNode("queryString")).ToNewScope(new InvocationExpressionBuilder("Add"))) {
					using (codeStack.NewScope(new ArgumentListBuilder())) {
						codeStack.With(new LiteralNode(queryKey));
						if (finalType.SpecialType == SpecialType.System_String) {
							codeStack.With(new IdentifierNode(variableName));
						} else {
							if (finalType.SpecialType == SpecialType.System_DateTime
								|| finalType.GetFullName() == "System.DateTimeOffset"
								|| finalType.GetFullName() == "System.DateOnly"
								|| finalType.GetFullName() == "System.TimeOnly") {
								using (codeStack.NewScope()) {
									codeStack.With(new IdentifierNode(variableName));
									if (type.IsNullableValueType(compilation)) {
										codeStack.With(new IdentifierNode("Value"));
									}
									codeStack.To(new InvocationExpressionBuilder("ISO8601String"));
								}
							} else if (finalType.SpecialType == SpecialType.System_String) {
								codeStack.With(new IdentifierNode(variableName));
							} else {
								codeStack.Begin().With(new IdentifierNode(variableName))
									.To(new InterpolatedStringBuilder())
									.End();
							}
						}
					}
				}
			}
			if (type.IsNullable(compilation)) { codeStack.End(); }
			return codeStack;
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) {
			return Convert(from);
		}
	}
}