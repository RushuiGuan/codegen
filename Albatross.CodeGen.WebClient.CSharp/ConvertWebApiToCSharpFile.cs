using Albatross.CodeAnalysis;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.CSharp {
	public class ConvertWebApiToCSharpFile : IConvertObject<ControllerInfo, FileDeclaration> {
		const string Client = "Client";
		private readonly Compilation compilation;
		private readonly CSharpWebClientSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertWebApiToCSharpFile(ICompilationFactory compilationFactory, ICodeGenSettingsFactory settingsFactory, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.compilation = compilationFactory.Get();
			this.settings = settingsFactory.Get<CSharpWebClientSettings>();
			this.typeConverter = typeConverter;
		}

		ITypeExpression GetMethodReturnType(ITypeSymbol typeSymbol) {
			if (typeSymbol.SpecialType == SpecialType.System_Void) {
				return Defined.Types.Task;
			} else {
				return new TypeExpression(Defined.Identifiers.Task) {
					GenericArguments = [typeConverter.Convert(typeSymbol),]
				};
			}
		}

		ListOfParameterDeclarations GetMethodParameters(MethodInfo method) {
			return new ListOfParameterDeclarations {
				method.Parameters.Select(param => new ParameterDeclaration {
					Name = new IdentifierNameExpression(param.Name),
					Type = typeConverter.Convert(param.Type),
				}),
				new ParameterDeclaration {
					Name = new IdentifierNameExpression("cancellationToken"),
					Type = Defined.Types.CancellationToken,
				}
			};
		}

		InterfaceDeclaration CreateInterface(ControllerInfo controller) {
			var interfaceName = GetInterfaceName(controller);
			return new InterfaceDeclaration(interfaceName) {
				IsPartial = true,
				Attributes = controller.IsObsolete ? [Defined.Attributes.Obsolete] : [],
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
				AccessModifier = settings.UseInternalProxy ? Defined.Keywords.Internal : Defined.Keywords.Public,
				BaseTypes = settings.UseInterface ? [new TypeExpression(GetInterfaceName(controller))] : [],
				Attributes = controller.IsObsolete ? [Defined.Attributes.Obsolete] : [],
				Constructors = CreateConstructors(controller).ToList(),
				Fields = [
					new FieldDeclaration {
						AccessModifier = Defined.Keywords.Public,
						IsConst = true,
						Type = new TypeExpression("string"),
						Name = new IdentifierNameExpression("ControllerPath"),
						Initializer = new StringLiteralExpression(controller.Route),
					},
					new FieldDeclaration {
						AccessModifier = Defined.Keywords.Private,
						IsConst = false,
						Type = Defined.Types.HttpClient,
						Name = new IdentifierNameExpression("client"),
					},
					new FieldDeclaration {
						AccessModifier = Defined.Keywords.Private,
						IsConst = false,
						Type = Defined.Types.JsonSerializerOptions,
						Name = new IdentifierNameExpression("jsonSerializerOptions"),
					},
				],
				Methods = from method in controller.Methods select CreateMethod(method),
			};
		}

		string GetInterfaceName(ControllerInfo controller) {
			return $"I{GetClassName(controller)}";
		}

		string GetClassName(ControllerInfo controller) {
			return $"{controller.ControllerName}{Client}";
		}

		string GetFilename(ControllerInfo controller) {
			return $"{GetClassName(controller)}.generated";
		}

		MethodDeclaration CreateMethod(MethodInfo method) {
			return new MethodDeclaration {
				AccessModifier = Defined.Keywords.Public,
				Attributes = method.IsObsolete ? [Defined.Attributes.Obsolete] : [],
				Name = new IdentifierNameExpression(method.Name),
				ReturnType = GetMethodReturnType(method.ReturnType),
				Parameters = GetMethodParameters(method),
				IsAsync = true,
				Body = {
					new AssignmentExpression {
							Left = new VariableDeclaration {
								Identifier = new IdentifierNameExpression("builder"),
							},
							Expression = new NewObjectExpression {
								Type = new TypeExpression(new IdentifierNameExpression("RequestBuilder"))
							}
						}.Chain(true, new InvocationExpression {
							CallableExpression = new IdentifierNameExpression("WithMethod"),
							Arguments = {
								new IdentifierNameExpression($"HttpMethod.{method.HttpMethod}")
							}
						})
						.Chain(true, new InvocationExpression {
							CallableExpression = new IdentifierNameExpression("WithRelativeUrl"),
							Arguments = {
								new StringInterpolationExpression {
									Items = { BuildRouteSegments(method.RouteSegments) },
								}
							}
						}),
					BuildQuery(method),
					BuildRequest(method),
				}
			};
		}

		IEnumerable<IExpression> BuildQuery(MethodInfo method) {
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				if (param.Type.TryGetCollectionElementType(compilation, out var elementType)) {
					yield return new ForEachExpression {
						IterationVariable = new VariableDeclaration {
							Identifier = new IdentifierNameExpression("item"),
						},
						Collection = new IdentifierNameExpression(param.Name),
						Body = { CreateAddQueryStringStatement(elementType, param.QueryKey, "item") }
					};
				} else {
					yield return CreateAddQueryStringStatement(param.Type, param.QueryKey, param.Name);
				}
			}
		}

		IEnumerable<ConstructorDeclaration> CreateConstructors(ControllerInfo from) {
			settings.ConstructorSettings.TryGetValue(from.Controller.Name, out var constructorSettings);
			if (constructorSettings == null) {
				settings.ConstructorSettings.TryGetValue("*", out constructorSettings);
			}
			if (constructorSettings?.Omit != true) {
				var constructor = new ConstructorDeclaration {
					Name = new IdentifierNameExpression(GetClassName(from)),
					Parameters = {
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("client"),
							Type = new TypeExpression("HttpClient"),
						}
					},
					Body = {
						new AssignmentExpression {
							Left = new IdentifierNameExpression("this.client"),
							Expression = new IdentifierNameExpression("client"),
						},
						new AssignmentExpression {
							Left = new IdentifierNameExpression("this.jsonSerializerOptions"),
							Expression = string.IsNullOrEmpty(constructorSettings?.CustomJsonSettings) ? new IdentifierNameExpression("DefaultJsonSerializerOptions.Value") : new IdentifierNameExpression(constructorSettings.CustomJsonSettings),
						}
					}
				};
				return [constructor];
			} else {
				return [];
			}
		}

		IExpression GetExecuteWithVoidFunction() {
			return new InvocationExpression {
				CallableExpression = new IdentifierNameExpression("this.client.Send") {
					GenericArguments = { Defined.Types.String }
				},
				Arguments = {
					new IdentifierNameExpression("request"),
					new IdentifierNameExpression("this.jsonSerializerOptions"),
					new IdentifierNameExpression("cancellationToken"),
				},
				UseAwaitOperator = true,
			};
		}

		IExpression GetExecuteWithReturnFunction(MethodInfo method) {
			string functionName;
			if (method.ReturnType.IsNullable(compilation)) {
				functionName = "this.client.Execute";
			} else if (method.ReturnType.IsValueType) {
				functionName = "this.client.ExecuteOrThrowStruct";
			} else {
				functionName = "this.client.ExecuteOrThrow";
			}
			return new ReturnExpression {
				Expression = new InvocationExpression {
					CallableExpression = new IdentifierNameExpression(functionName) {
						GenericArguments = { this.typeConverter.Convert(method.ReturnType) }
					},
					Arguments = {
						new IdentifierNameExpression("request"),
						new IdentifierNameExpression("this.jsonSerializerOptions"),
						new IdentifierNameExpression("cancellationToken"),
					},
					UseAwaitOperator = true,
				}
			};
		}

		IExpression GetExecuteFunction(MethodInfo method) {
			if (method.ReturnType.SpecialType == SpecialType.System_Void) {
				return GetExecuteWithVoidFunction();
			} else {
				return GetExecuteWithReturnFunction(method);
			}
		}

		IEnumerable<IExpression> BuildRequest(MethodInfo method) {
			var fromBody = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
			if (fromBody != null) {
				yield return new InvocationExpression {
					CallableExpression = fromBody.Type.SpecialType == SpecialType.System_String
						? new IdentifierNameExpression("builder.CreateStringRequest")
						: new IdentifierNameExpression("builder.CreateJsonRequest") {
							GenericArguments = {
								this.typeConverter.Convert(fromBody.Type)
							}
						},
					Arguments = {
						new IdentifierNameExpression(fromBody.Name)
					}
				};
			}
			yield return new UsingExpression {
				Resource = new AssignmentExpression {
					Left = new VariableDeclaration {
						Identifier = new IdentifierNameExpression("request"),
					},
					Expression = new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("builder.Build"),
					}
				}
			};
			yield return GetExecuteFunction(method);
		}

		public FileDeclaration Convert(ControllerInfo from) {
			return new FileDeclaration(GetFilename(from)) {
				Namespace = new NamespaceExpression(settings.Namespace),
				NullableEnabled = true,
				Imports = [
					new ImportExpression("Albatross.Http"),
					new ImportExpression("System.Net.Http"),
					new ImportExpression("Microsoft.Extensions.Logging"),
				],
				Interfaces = settings.UseInterface ? [CreateInterface(from)] : [],
				Classes = [CreateClass(from)],
			};
		}

		IEnumerable<IExpression> BuildRouteSegments(IEnumerable<IRouteSegment> segments) {
			yield return new IdentifierNameExpression("ControllerPath");
			if (segments.Any()) {
				yield return new StringLiteralExpression("/");
			}
			foreach (var segment in segments) {
				if (segment is RouteParameterSegment routeParam) {
					var type = routeParam.ParameterInfo?.Type;
					if (type.Is(compilation.DateTime()) || type.Is(compilation.DateTimeOffset()) || type.Is(compilation.TimeOnly()) || type.Is(compilation.DateOnly())) {
						yield return new InvocationExpression {
							CallableExpression = new IdentifierNameExpression($"{routeParam.Text}.ISO8601"),
						};
					} else {
						yield return new IdentifierNameExpression(routeParam.Text);
					}
				} else {
					yield return new StringLiteralExpression(segment.Text);
				}
			}
		}

		IExpression CreateAddQueryStringStatement(ITypeSymbol type, string queryKey, string variableName) {
			IdentifierNameExpression functionName;
			if (type.IsValueType && !type.IsNullable(compilation)) {
				functionName = new IdentifierNameExpression("builder.AddQueryString");
			} else {
				functionName = new IdentifierNameExpression("builder.AddQueryStringIfSet");
			}
			return new InvocationExpression {
				CallableExpression = functionName,
				Arguments = { new StringLiteralExpression(queryKey), new IdentifierNameExpression(variableName) }
			};
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) {
			return Convert(from);
		}
	}
}