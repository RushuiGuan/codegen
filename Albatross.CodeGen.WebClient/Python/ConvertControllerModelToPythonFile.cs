using Humanizer;
using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Python.Modifiers;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Text;
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

		private static readonly IIdentifierNameExpression AsyncClient = new QualifiedIdentifierNameExpression("AsyncClient", new ModuleSourceExpression("httpx"));

		public PythonFileDeclaration Convert(ControllerInfo model) {
			var fileName = $"{model.ControllerName.Underscore()}_client";
			return new PythonFileDeclaration(fileName) {
				Banner = [
					new CommentDeclaration("@generated"),
				],
				ClasseDeclarations = [
					new ClassDeclaration($"{model.ControllerName}Client") {
						Fields = [
							new FieldDeclaration("_client") {
								Type = new SimpleTypeExpression {
									Identifier = AsyncClient
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
							new SimpleTypeExpression{
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
			} else if (method.ReturnTypeText == "System.String") {
				return new ReturnExpression(new MultiPartIdentifierNameExpression("response", "text"));
			} else {
				return new ReturnExpression(
					new InvocationExpressionBuilder()
						.WithIdentifier(Defined.Identifiers.TypeAdapter)
						.AddArgument(this.typeConverter.Convert(method.ReturnType))
						.Chain("validate_python")
						.AddArgument(new InvocationExpression {
							CallableExpression = new MultiPartIdentifierNameExpression("response", "json")
						}).Build()
				);
			}
		}

		IExpression BuildRelativeUrl(MethodInfo method) {
			var builder = new ScopedVariableExpressionBuilder().WithName("relative_url")
				.WithExpression(new StringInterpolationExpression(method.RouteSegments.Select(x => BuildRouteSegment(method, x))));
			return builder.Build();
		}

		IExpression BuildQueryParameters(MethodInfo method) {
			var properties = new List<KeyValuePairExpression>();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				properties.Add(BuildQueryStringParameter(param));
			}
			if (properties.Any()) {
				return new ScopedVariableExpressionBuilder().WithName("params").WithExpression(new DictionaryValueExpression(properties)).Build();
			} else {
				return new NoOpExpression();
			}
		}

		/// <summary>
		/// This method will generate this
		/// { dates:dates.map(x=>format(x, "yyyy-MM-dd")) }
		/// { d:dates.map(x=>format(x, "yyyy-MM-dd")) }
		/// { value }
		/// { v: value }
		/// </summary>
		KeyValuePairExpression BuildQueryStringParameter(ParameterInfo parameter) {
			IExpression value;
			if (parameter.Type.TryGetCollectionElementType(out var elementType) && IsDate(elementType!)) {
				value = new ListComprehensionExpression {
					VariableName = "d",
					IterableExpression = new IdentifierNameExpression(parameter.Name.Underscore()),
					Expression = new InvocationExpression {
						CallableExpression = new MultiPartIdentifierNameExpression("d", "isoformat"),
					}
				};
			} else {
				value = BuildParamValue(parameter.Name.Underscore(), parameter.Type);
			}
			return new KeyValuePairExpression(new StringLiteralExpression(parameter.QueryKey), value);
		}

		IExpression BuildRouteSegment(MethodInfo method, IRouteSegment segment) {
			if (segment is RouteParameterSegment parameterSegment) {
				return BuildParamValue(segment.Text, parameterSegment.RequiredParameterInfo.Type);
			} else {
				return new StringLiteralExpression(segment.Text);
			}
		}

		InvocationExpression FormattedDate(string variableName) {
			return new InvocationExpression {
				CallableExpression = new MultiPartIdentifierNameExpression(variableName, "isoformat"),
			};
		}

		IExpression BuildParamValue(string variableName, ITypeSymbol elementType) {
			IExpression value;
			var typeName = elementType!.GetFullName();
			if (typeName == typeof(TimeOnly).FullName ||
				typeName == typeof(DateOnly).FullName ||
				typeName == typeof(DateTime).FullName || typeName == typeof(DateTimeOffset).FullName) {
				value = FormattedDate(variableName);
			} else {
				value = new IdentifierNameExpression(variableName);
			}
			return value;
		}

		bool IsDate(ITypeSymbol type) {
			var typeName = type.GetFullName();
			return typeName == typeof(TimeOnly).FullName
				   || typeName == typeof(DateOnly).FullName
				   || typeName == typeof(DateTime).FullName
				   || typeName == typeof(DateTimeOffset).FullName;
		}

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
						Assignment = new DictionaryValueExpression(new KeyValuePairExpression("Content-Type", "text/plain"))
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