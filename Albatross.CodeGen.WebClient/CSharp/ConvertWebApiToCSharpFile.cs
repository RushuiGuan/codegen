using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeAnalysis.Syntax;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.CSharp;
using Microsoft.CodeAnalysis;
using System.Linq;
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

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
		
		InterfaceDeclaration CreateInterface(ControllerInfo controller) {
			var interfaceName = $"I{controller.ControllerName}{ProxyService}";
			return new InterfaceDeclaration(interfaceName) {
				IsPartial = true,
				Attributes = new List<AttributeExpression>().AddIf(controller.IsObsolete, Defined.Attributes.Obsolete),
				Methods = from method in controller.Methods select new MethodDeclaration {
					Name = new IdentifierNameExpression(method.Name),
					ReturnType = GetMethodReturnType(method.ReturnType),
					Parameters = method.Parameters.Select(param => new ParameterDeclaration {
						Name = new IdentifierNameExpression(param.Name),
						Type = typeConverter.Convert(param.Type),
					}).ToList(),
				}
			};
		}

		public FileDeclaration Convert(ControllerInfo from) {
			var proxyClassName = from.ControllerName + ProxyService;
			var interfaceName = $"I{proxyClassName}";
			var fileName = $"{proxyClassName}.generated";
			var file = new FileDeclaration(fileName) {
				Imports =new List<UsingExpression> {
					new UsingExpression("Albatross.Dates"),
					new UsingExpression("System.Net.Http"),
					new UsingExpression("System.Threading.Tasks"),
					new UsingExpression("Microsoft.Extensions.Logging"),
					new UsingExpression("Albatross.WebClient"),
					new UsingExpression("System.Collections.Specialized"),
				},
			};
			if (settings.CSharpWebClientSettings.UseInterface) {
				var @interface = new InterfaceDeclaration(interfaceName) {
					IsPartial = true,
				};
				file.Interfaces.Add(@interface);
				if (from.IsObsolete) {
					@interface.Attributes.Add(new AttributeExpression {
						CallableExpression = new IdentifierNameExpression("System.ObsoleteAttribute"),
					});
				}
				foreach (var method in from.Methods) {
					var declaration = new MethodDeclaration {
						Name = new IdentifierNameExpression(method.Name),
						ReturnType = method.ReturnType.SpecialType == SpecialType.System_Void
							? new TypeExpression("Task") : new TypeExpression("Task") {
								GenericArguments = [typeConverter.Convert(method.ReturnType),]
							},
						Parameters = method.Parameters.Select(param => new ParameterDeclaration {
							Name = new IdentifierNameExpression(param.Name),
							Type = typeConverter.Convert(param.Type),
						}).ToList(),
					};
				}
			}
			var classDeclaration = new ClassDeclaration(proxyClassName) {
				IsPartial = true,
				AccessModifier = settings.CSharpWebClientSettings.UseInternalProxy ? AccessModifierKeyword.Internal : AccessModifierKeyword.Public,
				BaseTypes = [new TypeExpression("ClientBase")],
			};
			if (settings.CSharpWebClientSettings.UseInterface) {
				classDeclaration.BaseTypes.Add(new TypeExpression(interfaceName));
			}
			if (from.IsObsolete) {
				classDeclaration.AttributeExpressions.Add(new AttributeExpression {
					CallableExpression = new IdentifierNameExpression("System.ObsoleteAttribute"),
				});
			}
			settings.CSharpWebClientSettings.ConstructorSettings.TryGetValue(from.Controller.Name, out var constructorSettings);
			if (constructorSettings == null) {
				settings.CSharpWebClientSettings.ConstructorSettings.TryGetValue("*", out constructorSettings);
			}
			if (constructorSettings?.Omit != true) {
				var constructor = new ConstructorDeclaration {
					Name = new IdentifierNameExpression(proxyClassName),
					Parameters = [
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("logger"),
							Type = new TypeExpression("ILogger", proxyClassName),
						},
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("client"),
							Type = new TypeExpression("HttpClient"),
						},
					],
					BaseConstructorInvocation = new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("base"),
						ArgumentList = {
							new IdentifierNameExpression("logger"),
							new IdentifierNameExpression("client"),
						}
					}
				};
				if (!string.IsNullOrEmpty(constructorSettings?.CustomJsonSettings)) {
					constructor.BaseConstructorInvocation.ArgumentList.Add(new IdentifierNameExpression(constructorSettings!.CustomJsonSettings!));
				}
				classDeclaration.Constructors.Add(constructor);
				classDeclaration.Fields.Add(new FieldDeclaration {
					AccessModifier = AccessModifierKeyword.Public,
					IsConst = true,
					Type = new TypeExpression("string"),
					Name = new IdentifierNameExpression("ControllerPath"),
					Initializer = new StringLiteralExpression(from.Route),
				});
				foreach (var method in from.Methods) {
					var methodDeclaration = new MethodDeclaration {
						Name = new IdentifierNameExpression(method.Name),
						ReturnType = method.ReturnType.SpecialType == SpecialType.System_Void
							? new TypeExpression("Task") : new TypeExpression("Task") {
								GenericArguments = [typeConverter.Convert(method.ReturnType),]
							},
						AccessModifier = AccessModifierKeyword.Public,
						IsAsync = true,
						Parameters = method.Parameters.Select(param => new ParameterDeclaration {
							Name = new IdentifierNameExpression(param.Name),
							Type = typeConverter.Convert(param.Type),
						}).ToList(),
					};
					if (method.IsObsolete) {
						methodDeclaration.Attributes.Add(new AttributeExpression {
							CallableExpression = new IdentifierNameExpression("System.ObsoleteAttribute"),
						});
					}
					classDeclaration.Methods.Add(methodDeclaration);
					var builder = new CompositeNodeBuilder();
					builder.Add(() => {
						var variable = new VariableDeclaration {
							Type = new TypeExpression("string"),
							Identifier = new IdentifierNameExpression("path"),
							Assignment = new StringInterpolationExpression(new IdentifierNameExpression("ControllerPath"))
						};
						return variable;
					});
				}
			}

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
			foreach(var segment in segments) {
				if (segment is RouteParameterSegment routeParam) {
					var type = routeParam.ParameterInfo?.Type;
					if (type.Is(compilation.DateTime()) || type.Is(compilation.DateTimeOffset()) || type.Is(compilation.TimeOnly()) || type.Is(compilation.DateOnly())) {
						yield return new InvocationExpression {
							CallableExpression = new IdentifierNameExpression("ISO8601String"),
							ArgumentList = { new IdentifierNameExpression(routeParam.Text) }
						};
					} else {
						yield return new IdentifierNameExpression(routeParam.Text);
					}
				}else {
					yield return new StringLiteralExpression(segment.Text);
				}
			}
		}

		CodeStack BuildRouteSegment(CodeStack cs, MethodInfo method, IRouteSegment routeSegment) {
			using (cs.NewScope()) {
				if (routeSegment is RouteParameterSegment routeParam) {
					var type = routeParam.ParameterInfo?.Type.GetFullName();
					if (type == "System.DateTime" || type == "System.DateTimeOffset" || type == "System.DateOnly" || type == "System.TimeOnly") {
						cs.With(new IdentifierNode(routeParam.Text)).To(new InvocationExpressionBuilder("ISO8601String"));
					} else {
						cs.With(new IdentifierNode(routeParam.Text));
					}
				} else {
					cs.With(new LiteralNode(routeSegment.Text));
				}
			}
			return cs;
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