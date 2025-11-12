using Humanizer;
using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Python.Modifiers;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertControllerModelToPythonFile : IConvertObject<ControllerInfo, PythonFileDeclaration> {
		private readonly PythonWebClientSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertControllerModelToPythonFile(CodeGenSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.settings = settings.PythonWebClientSettings;
			this.typeConverter = typeConverter;
		}

		private static readonly IIdentifierNameExpression asyncClient = new QualifiedIdentifierNameExpression("AsyncClient", new ModuleSourceExpression("httpx"));

		public PythonFileDeclaration Convert(ControllerInfo model) {
			var fileName = $"{model.ControllerName.Underscore()}_client";
			return new PythonFileDeclaration(fileName) {
				Banner = [
					new CommentDeclaration("@generated"),
				],
				ClasseDeclarations = [
					new ClassDeclaration($"{model.ControllerName}Client") {
						Decorators = model.IsObsolete ? [new DecoratorExpression { CallableExpression = Defined.Identifiers.Deprecated, }] : [],
						Fields = [
							new FieldDeclaration("_client") {
								Type = new SimpleTypeExpression {
									Identifier = asyncClient
								}
							},
						],
						Constructor = CreateConstructor(model),
						Methods = new MethodDeclaration[] {
							CreateCloseMethod(),
							CreateAsyncEnterMethod(),
							CreateAsyncExitMethod()
						}.Concat(GroupMethods(model).Select(x => BuildMethod(x.Method, x.Index))).ToArray(),
					}
				],
			};
		}

		MethodDeclaration CreateConstructor(ControllerInfo model) => new ConstructorDeclaration() {
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration> {
				Nodes = [
					Defined.Parameters.Self,
					new ParameterDeclaration {
						Identifier = new IdentifierNameExpression("base_url"),
						Type = Defined.Types.String
					},
					new ParameterDeclaration {
						Identifier = new IdentifierNameExpression("auth"),
						Type = new MultiTypeExpression(
							new SimpleTypeExpression {
								Identifier = new QualifiedIdentifierNameExpression("Auth", new ModuleSourceExpression("httpx"))
							},
							Defined.Types.None
						),
						DefaultValue = new NoneLiteralExpression(),
					},
				],
			},
			Body = new CompositeExpression {
				Items = [
					new ScopedVariableExpression {
						Identifier = new IdentifierNameExpression("base_url"),
						Assignment = new StringInterpolationExpression(
							new InvocationExpression {
								CallableExpression = new MultiPartIdentifierNameExpression("base_url", "rstrip"),
								ArgumentList = new ListOfSyntaxNodes<IExpression>(new StringLiteralExpression("/", true)),
							},
							new StringLiteralExpression("/"),
							new StringLiteralExpression(model.Route)
						),
					},
					new ScopedVariableExpression {
						Identifier = new MultiPartIdentifierNameExpression("self", "_client"),
						Assignment = new InvocationExpression {
							CallableExpression = new QualifiedIdentifierNameExpression("AsyncClient", new ModuleSourceExpression("httpx")),
							ArgumentList = new ListOfSyntaxNodes<IExpression>(
								new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("base_url"),
									Assignment = new IdentifierNameExpression("base_url"),
								},
								new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("auth"),
									Assignment = new IdentifierNameExpression("auth")
								}
							),
						}
					}
				]
			},
		};

		MethodDeclaration CreateCloseMethod() => new MethodDeclaration("close") {
			Modifiers = [new AsyncModifier()],
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(Defined.Parameters.Self),
			ReturnType = Defined.Types.None,
			Body = new InvocationExpressionBuilder()
				.Await()
				.WithMultiPartFunctionName("self", "_client", "aclose")
				.Build(),
		};

		MethodDeclaration CreateAsyncEnterMethod() => new MethodDeclaration("__aenter__") {
			Modifiers = [new AsyncModifier()],
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(Defined.Parameters.Self),
			ReturnType = Defined.Types.Self,
			Body = new ReturnExpression(Defined.Identifiers.Self),
		};

		MethodDeclaration CreateAsyncExitMethod() => new MethodDeclaration("__aexit__") {
			Modifiers = [new AsyncModifier()],
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(
				Defined.Parameters.Self,
				new ParameterDeclaration {
					Identifier = new IdentifierNameExpression("exc_type"),
					Type = Defined.Types.None
				},
				new ParameterDeclaration {
					Identifier = new IdentifierNameExpression("exc_value"),
					Type = Defined.Types.None
				},
				new ParameterDeclaration {
					Identifier = new IdentifierNameExpression("traceback"),
					Type = Defined.Types.None
				}),
			ReturnType = Defined.Types.None,
			Body = new InvocationExpressionBuilder().Await().WithMultiPartFunctionName("self", "close").Build(),
		};

		// has to do this since python doesn't support methods of the same name
		IEnumerable<(MethodInfo Method, int Index)> GroupMethods(ControllerInfo model) {
			foreach (var group in model.Methods.GroupBy(x => x.Name)) {
				var index = 0;
				foreach (var item in group) {
					yield return (item, index++);
				}
			}
		}

		MethodDeclaration BuildMethod(MethodInfo method, int index) {
			var returnType = this.typeConverter.Convert(method.ReturnType);
			var name = index == 0 ? method.Name.Underscore() : $"{method.Name.Underscore()}{index}";
			return new MethodDeclaration(name) {
				Modifiers = [new AsyncModifier()],
				Decorators = method.IsObsolete ? [new DecoratorExpression { CallableExpression = Defined.Identifiers.Deprecated, }] : Array.Empty<DecoratorExpression>(),
				ReturnType = returnType,
				Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(
					method.Parameters.Select(x => new ParameterDeclaration {
						Identifier = new IdentifierNameExpression(x.Name.Underscore()),
						Type = this.typeConverter.Convert(x.Type)
					})).WithSelf(),
				Body = new CompositeExpression(
					BuildRelativeUrl(method),
					BuildQueryParameters(method),
					CreateHttpInvocationExpression(method),
					BuildReturnValue(method)
				),
			};
		}

		IExpression BuildReturnValue(MethodInfo method) {
			if (method.ReturnType.SpecialType == SpecialType.System_Void) {
				return new NoOpExpression();
			}
			ExpressionBuilder builder = new CompositeExpressionBuilder();
			if (method.ReturnType.IsNullable()) {
				builder.Add(() => new IfElseCodeBlockExpression {
					Condition = new ConditionExpression("==") {
						Left = new MultiPartIdentifierNameExpression("response", "status_code"),
						Right = new IntLiteralExpression(204),
					},
					CodeBlock = new ReturnExpression(new NoneLiteralExpression()),
				});
			}
			if (method.ReturnTypeText == "System.String") {
				builder.Add(() => new ReturnExpression(new MultiPartIdentifierNameExpression("response", "text")));
			} else {
				builder.Add(() =>
					new ReturnExpression(
						new InvocationExpressionBuilder()
							.WithIdentifier(Defined.Identifiers.TypeAdapter)
							.AddArgument(this.typeConverter.Convert(method.ReturnType))
							.Chain("validate_python")
							.AddArgument(new InvocationExpression {
								CallableExpression = new MultiPartIdentifierNameExpression("response", "json")
							}).Build()
					));
			}
			return builder.BuildAll();
		}

		IExpression BuildRelativeUrl(MethodInfo method) {
			var builder = new ScopedVariableExpressionBuilder().WithName("relative_url")
				.WithExpression(new StringInterpolationExpression(method.RouteSegments.Select(x => BuildRouteSegment(method, x))));
			return builder.Build();
		}

		IExpression BuildQueryParameters(MethodInfo method) {
			var properties = new List<KeyValuePairExpression>();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				var value = BuildParamValue(param.Name.Underscore(), param.Type, false);
				properties.Add(new KeyValuePairExpression(new StringLiteralExpression(param.QueryKey), value));
			}
			if (properties.Any()) {
				return new ScopedVariableExpressionBuilder().WithName("params").WithExpression(new DictionaryExpression(properties) { LineBreak = true, }).Build();
			} else {
				return new NoOpExpression();
			}
		}

		IExpression BuildRouteSegment(MethodInfo method, IRouteSegment segment) {
			if (segment is RouteParameterSegment parameterSegment) {
				return BuildParamValue(segment.Text.Underscore(), parameterSegment.RequiredParameterInfo.Type, false);
			} else {
				return new StringLiteralExpression(segment.Text);
			}
		}

		IExpression FormattedDate(string variableName, bool isDateTime) {
			if (isDateTime) {
				// if there is timezone, convert it to utc -> isoformaat and replace '+00:00' with 'Z' 
				// value.astimezone(timezone.utc).isoformat().replace('+00:00', 'Z') if value.tzinfo else value.isoformat()
				return new TernaryExpression {
					LineBreak = true,
					TrueExpression = new InvocationExpressionBuilder()
						.WithMultiPartFunctionName(variableName, "astimezone")
						.AddArgument(Defined.Identifiers.TimeZoneUtc)
						.Chain("isoformat")
						.Chain("replace").AddArgument(new StringLiteralExpression("+00:00"))
						.AddArgument(new StringLiteralExpression("Z")).BuildAll(),
					Condition = new MultiPartIdentifierNameExpression(variableName, "tzinfo"),
					FalseExpression = new InvocationExpression {
						CallableExpression = new MultiPartIdentifierNameExpression(variableName, "isoformat"),
					}
				};
			} else {
				return new InvocationExpression {
					CallableExpression = new MultiPartIdentifierNameExpression(variableName, "isoformat")
				};
			}
		}

		IExpression BuildParamValue(string variableName, ITypeSymbol parameterType, bool useEmptyStringForNullValue) {
			if (IsDate(parameterType, out var isDateTime)) {
				return FormattedDate(variableName, isDateTime);
			} else if (IsEnum(parameterType)) {
				return new MultiPartIdentifierNameExpression(variableName, "value");
			} else if (parameterType.SpecialType == SpecialType.System_String && useEmptyStringForNullValue) {
				return new TernaryExpression {
					Condition = new TypeCheckExpression(true) {
						Expression = new IdentifierNameExpression(variableName),
						Type = Defined.Types.None,
					},
					TrueExpression = new IdentifierNameExpression(variableName),
					FalseExpression = new StringLiteralExpression("")
				};
			} else if (parameterType.TryGetNullableValueType(out var valueType)) {
				if (IsDate(valueType!, out isDateTime)) {
					return new TernaryExpression {
						LineBreak = true,
						Condition = new TypeCheckExpression(true) {
							Expression = new IdentifierNameExpression(variableName),
							Type = Defined.Types.None,
						},
						TrueExpression = FormattedDate(variableName, isDateTime),
						FalseExpression = useEmptyStringForNullValue ? new StringLiteralExpression("") : new NoneLiteralExpression(),
					};
				} else if (IsEnum(valueType!)) {
					return new TernaryExpression {
						Condition =  new TypeCheckExpression(true){
							Expression = new IdentifierNameExpression(variableName),
							Type = Defined.Types.None,
						},
						TrueExpression = new MultiPartIdentifierNameExpression(variableName, "value"),
						FalseExpression = useEmptyStringForNullValue ? new StringLiteralExpression("") : new NoneLiteralExpression(),
					};
				} else {
					if (useEmptyStringForNullValue) {
						return new TernaryExpression {
							Condition = new TypeCheckExpression(true) {
								Expression = new IdentifierNameExpression(variableName),
								Type = Defined.Types.None,
							},
							TrueExpression = new IdentifierNameExpression(variableName),
							FalseExpression = new StringLiteralExpression("")
						};
					}
				}
			} else if (parameterType.TryGetCollectionElementType(out var elementType) && (IsDate(elementType!, out _) || IsEnum(elementType!) || elementType!.IsNullable())) {
				return new ListComprehensionExpression {
					LineBreak = true,
					VariableName = "x",
					IterableExpression = new IdentifierNameExpression(variableName),
					Expression = BuildParamValue("x", elementType!, true),
				};
			}
			return new IdentifierNameExpression(variableName);
		}

		bool IsDate(ITypeSymbol type, out bool isDateTime) {
			var typeName = type.GetFullName();
			if (typeName == typeof(DateTime).FullName || typeName == typeof(DateTimeOffset).FullName) {
				isDateTime = true;
				return true;
			} else {
				isDateTime = false;
				return typeName == typeof(TimeOnly).FullName || typeName == typeof(DateOnly).FullName;
			}
		}

		bool IsEnum(ITypeSymbol type) => type.TypeKind == TypeKind.Enum;

		IExpression CreateHttpInvocationExpression(MethodInfo method) {
			var builder = new InvocationExpressionBuilder();
			switch (method.HttpMethod) {
				case My.HttpMethodGet:
					builder.WithMultiPartFunctionName("self", "_client", "get");
					break;
				case My.HttpMethodPost:
					builder.WithMultiPartFunctionName("self", "_client", "post");
					break;
				case My.HttpMethodPatch:
					builder.WithMultiPartFunctionName("self", "_client", "patch");
					break;
				case My.HttpMethodPut:
					builder.WithMultiPartFunctionName("self", "_client", "put");
					break;
				case My.HttpMethodDelete:
					builder.WithMultiPartFunctionName("self", "_client", "delete");
					break;
			}
			builder.Await();
			// add relativeUrl parameter
			builder.AddArgument(new IdentifierNameExpression("relative_url"));
			// add from body parameter if it exists
			var fromBodyParameter = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
			if (fromBodyParameter != null) {
				if (fromBodyParameter.TypeText == "System.String") {
					// content=text,  # raw body, not JSON-encoded
					// headers={"Content-Type": "text/plain"},
					builder.AddArgument(new ScopedVariableExpression {
						Identifier = new IdentifierNameExpression("headers"),
						Assignment = new DictionaryExpression(new KeyValuePairExpression("Content-Type", "text/plain"))
					});
					if (fromBodyParameter.Type.IsNullable()) {
						builder.AddArgument(new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("content"),
							Assignment = new TernaryExpression {
								TrueExpression = new IdentifierNameExpression(fromBodyParameter.Name.Underscore()),
								Condition = new TypeCheckExpression(true) {
									Expression = new IdentifierNameExpression(fromBodyParameter.Name.Underscore()),
									Type = Defined.Types.None,
								},
								FalseExpression = new StringLiteralExpression(""),
							}
						});
					} else {
						builder.AddArgument(new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("content"),
							Assignment = new IdentifierNameExpression(fromBodyParameter.Name.Underscore())
						});
					}
				} else {
					// TypeAdapter(datetime).dump_python(datetime.now(), mode="json")
					builder.AddArgument(new ScopedVariableExpression {
						Identifier = new IdentifierNameExpression("json"),
						Assignment = new InvocationExpressionBuilder()
							.WithIdentifier(Defined.Identifiers.TypeAdapter)
							.AddArgument(this.typeConverter.Convert(fromBodyParameter.Type))
							.Chain("dump_python")
							.AddArgument(new IdentifierNameExpression(fromBodyParameter.Name.Underscore()))
							.AddArgument(new ScopedVariableExpression {
								Identifier = new IdentifierNameExpression("mode"),
								Assignment = new StringLiteralExpression("json")
							})
							.Build()
					});
				}
			}
			if (method.HasQueryStringParameter) {
				// build query string
				builder.AddArgument(
					new ScopedVariableExpressionBuilder()
						.WithName("params")
						.WithExpression(new IdentifierNameExpression("params")).Build()).Await();
			}
			return new ScopedVariableExpressionBuilder()
				.WithName("response").WithExpression(builder.Build())
				.Add(() => new InvocationExpressionBuilder().WithMultiPartFunctionName("response", "raise_for_status").Build())
				.BuildAll();
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) => this.Convert(from);
	}
}