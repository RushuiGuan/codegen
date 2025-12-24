using Albatross.CodeAnalysis;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Python.Modifiers;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Humanizer;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Albatross.CodeGen.WebClient.Python {
	public class ConvertControllerModelToPythonFile : IConvertObject<ControllerInfo, PythonFileDeclaration> {
		private readonly PythonWebClientSettings settings;
		private readonly Compilation compilation;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertControllerModelToPythonFile(Compilation compilation, PythonWebClientSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.settings = settings;
			this.compilation = compilation;
			this.typeConverter = typeConverter;
		}

		private static readonly IIdentifierNameExpression asyncClient = new QualifiedIdentifierNameExpression("AsyncClient", Defined.Sources.Httpx);
		private static readonly IIdentifierNameExpression syncClient = new QualifiedIdentifierNameExpression("Session", Defined.Sources.Requests);

		public PythonFileDeclaration Convert(ControllerInfo model) {
			if (!settings.HttpClientClassNameMapping.TryGetValue(model.Controller.GetFullName(), out var clientClassName)) {
				clientClassName = $"{model.ControllerName}Client";
			}
			var fileName = clientClassName.Underscore();
			return new PythonFileDeclaration(fileName) {
				Banner = [
					new CommentDeclaration("@generated"),
				],
				Classes = [
					new ClassDeclaration(clientClassName) {
						DocString = model.RequiresAuthentication ? new DocStringExpression("Authentication required") : null,
						Decorators = model.IsObsolete ? [new DecoratorExpression { CallableExpression = Defined.Identifiers.Deprecated, }] : [],
						Fields = [
							new FieldDeclaration("_client") {
								Type = new SimpleTypeExpression {
									Identifier = settings.Async ? asyncClient : syncClient
								}
							},
							new FieldDeclaration("_base_url") {
								Type = Defined.Types.String
							},
						],
						Constructor = CreateConstructor(model),
						Methods = new[] {
							CreateCloseMethod(),
							CreateEnterMethod(),
							CreateExitMethod()
						}.Concat(GroupMethods(model).Select(x => BuildMethod(x.Method, x.Index))).ToArray(),
					}
				],
			};
		}

		MethodDeclaration CreateConstructor(ControllerInfo model) => new ConstructorDeclaration() {
			Parameters = new ListOfNodes<ParameterDeclaration> {
				Defined.Parameters.Self,
				new ParameterDeclaration {
					Identifier = new IdentifierNameExpression("base_url"),
					Type = Defined.Types.String
				},
				new ParameterDeclaration {
					Identifier = new IdentifierNameExpression("auth"),
					Type = new MultiTypeExpression{
						new SimpleTypeExpression {
							Identifier = settings.Async ? new QualifiedIdentifierNameExpression("Auth", Defined.Sources.Httpx) : new QualifiedIdentifierNameExpression("AuthBase", Defined.Sources.RequestsAuth)
						},
						Defined.Types.None
					},
					DefaultValue = new NoneLiteralExpression(),
				},
			},
			Body = new CodeBlock {
				new ScopedVariableExpression {
					Identifier = new MultiPartIdentifierNameExpression("self", "_base_url"),
					Assignment = new StringInterpolationExpression(
						new InvocationExpression {
							CallableExpression = new MultiPartIdentifierNameExpression("base_url", "rstrip"),
							Arguments = new ListOfNodes<IExpression> {
								new StringLiteralExpression("/", true)
							},
						},
						new StringLiteralExpression("/"),
						new StringLiteralExpression(model.Route)
					),
				},
				new ScopedVariableExpression {
					Identifier = new MultiPartIdentifierNameExpression("self", "_client"),
					Assignment = new InvocationExpression {
						CallableExpression = settings.Async ? asyncClient : syncClient,
						Arguments = settings.Async
							? new ListOfNodes<IExpression> {
								new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("base_url"),
									Assignment = new MultiPartIdentifierNameExpression("self", "_base_url"),
								},
								new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("auth"),
									Assignment = new IdentifierNameExpression("auth")
								}
							}
							: new ListOfNodes<IExpression>(),
					}
				},
				settings.Async
					? new NoOpExpression()
					: new ScopedVariableExpression {
						Identifier = new MultiPartIdentifierNameExpression("self", "_client", "auth"),
						Assignment = new IdentifierNameExpression("auth"),
					},
			},
		};

		MethodDeclaration CreateCloseMethod() => new MethodDeclaration("close") {
			Modifiers = settings.Async ? [new AsyncKeyword()] : [],
			Parameters = new ListOfNodes<ParameterDeclaration> {
				Defined.Parameters.Self
			},
			ReturnType = Defined.Types.None,
			Body = new InvocationExpression {
				UseAwaitOperator = settings.Async,
				CallableExpression = new MultiPartIdentifierNameExpression("self", "_client", settings.Async ? "aclose" : "close")
			},
		};

		MethodDeclaration CreateEnterMethod() => new MethodDeclaration(settings.Async ? "__aenter__" : "__enter__") {
			Modifiers = settings.Async ? [new AsyncKeyword()] : [],
			Parameters = new ListOfNodes<ParameterDeclaration> { Defined.Parameters.Self },
			ReturnType = Defined.Types.Self,
			Body = new ReturnExpression(Defined.Identifiers.Self),
		};

		MethodDeclaration CreateExitMethod() => new MethodDeclaration(settings.Async ? "__aexit__" : "__exit__") {
			Modifiers = settings.Async ? [new AsyncKeyword()] : [],
			Parameters = new ListOfNodes<ParameterDeclaration> {
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
				}
			},
			ReturnType = Defined.Types.None,
			Body = new InvocationExpression {
				UseAwaitOperator = settings.Async,
				CallableExpression = new MultiPartIdentifierNameExpression("self", "close")
			},
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
				DocString = method.RequiresAuthentication ? new DocStringExpression("Authentication required") : null,
				Modifiers = settings.Async ? [new AsyncKeyword()] : [],
				Decorators = method.IsObsolete ? [new DecoratorExpression { CallableExpression = Defined.Identifiers.Deprecated, }] : Array.Empty<DecoratorExpression>(),
				ReturnType = returnType,
				Parameters = {
					Defined.Parameters.Self,
					method.Parameters.Select(x => new ParameterDeclaration {
						Identifier = new IdentifierNameExpression(x.Name.Underscore()),
						Type = this.typeConverter.Convert(x.Type)
					})
				},
				Body = new CodeBlock{
					BuildRequestUrl(method),
					BuildQueryParameters(method),
					CreateHttpInvocationExpression(method),
					BuildReturnValue(method)
				},
			};
		}

		IExpression BuildReturnValue(MethodInfo method) {
			if (method.ReturnType.SpecialType == SpecialType.System_Void) {
				return new NoOpExpression();
			}
			var builder = new List<IExpression>();
			if (method.ReturnType.IsNullable(compilation)) {
				builder.Add(new IfElseCodeBlockExpression {
					Condition = new ConditionExpression("==") {
						Left = new MultiPartIdentifierNameExpression("response", "status_code"),
						Right = new IntLiteralExpression(204),
					},
					CodeBlock = new ReturnExpression(new NoneLiteralExpression()),
				});
			}
			if (method.ReturnTypeText == "System.String") {
				builder.Add(new ReturnExpression(new MultiPartIdentifierNameExpression("response", "text")));
			} else {
				builder.Add(new ReturnExpression(
					new InvocationExpression {
						CallableExpression = Defined.Identifiers.TypeAdapter,
						Arguments = { this.typeConverter.Convert(method.ReturnType) }
					}.Chain(false, new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("validate_python"),
						Arguments = {
							new InvocationExpression {
								CallableExpression = new MultiPartIdentifierNameExpression("response", "json")
							}
						}
					})
				));
			}
			return new CodeBlock { builder };
		}

		IExpression BuildRequestUrl(MethodInfo method) {
			var segments = new List<IExpression>();
			if (!settings.Async) {
				segments.Add(new MultiPartIdentifierNameExpression("self", "_base_url"));
				segments.Add(new StringLiteralExpression("/"));
			}
			segments.AddRange(method.RouteSegments.Select(x => BuildRouteSegment(method, x)));
			return new ScopedVariableExpression {
				Identifier = new IdentifierNameExpression("request_url"),
				Assignment = new StringInterpolationExpression(segments)
			};
		}

		IExpression BuildQueryParameters(MethodInfo method) {
			var properties = new List<KeyValuePairExpression>();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				var value = BuildParamValue(param.Name.Underscore(), param.Type, false);
				properties.Add(new KeyValuePairExpression(new StringLiteralExpression(param.QueryKey), value));
			}
			if (properties.Any()) {
				return new ScopedVariableExpression {
					Identifier = new IdentifierNameExpression("params"),
					Assignment = new DictionaryExpression(properties) { LineBreak = true, }
				};
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
					TrueExpression = new InvocationExpression {
						CallableExpression = new MultiPartIdentifierNameExpression(variableName, "astimezone"),
						Arguments = { Defined.Identifiers.TimeZoneUtc }
					}.Chain(false, new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("isoformat"),
					}).Chain(false, new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("replace"),
						Arguments = {
							new StringLiteralExpression("+00:00"),
							new StringLiteralExpression("Z")
						}
					}),
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
			} else if (parameterType.TryGetNullableValueType(compilation, out var valueType)) {
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
						Condition = new TypeCheckExpression(true) {
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
			} else if (parameterType.TryGetCollectionElementType(compilation, out var elementType) && (IsDate(elementType!, out _) || IsEnum(elementType!) || elementType!.IsNullable(compilation))) {
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

		IIdentifierNameExpression GetHttpRequestMethodName(MethodInfo method) {
			return method.HttpMethod switch {
				My.HttpMethodGet => new MultiPartIdentifierNameExpression("self", "_client", "get"),
				My.HttpMethodPost => new MultiPartIdentifierNameExpression("self", "_client", "post"),
				My.HttpMethodPatch => new MultiPartIdentifierNameExpression("self", "_client", "patch"),
				My.HttpMethodPut => new MultiPartIdentifierNameExpression("self", "_client", "put"),
				My.HttpMethodDelete => new MultiPartIdentifierNameExpression("self", "_client", "delete"),
				_ => throw new NotSupportedException($"HTTP method {method.HttpMethod} is not supported."),
			};
		}

		IExpression CreateHttpInvocationExpression(MethodInfo method) {
			var fromBodyParameter = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
			var expression = new InvocationExpression {
				CallableExpression = GetHttpRequestMethodName(method),
				UseAwaitOperator = settings.Async,
				Arguments = {
					new IdentifierNameExpression("request_url"), {
						fromBodyParameter != null, () => {
							var list = new List<IExpression>();
							if (fromBodyParameter!.Type.SpecialType == SpecialType.System_String) {
								// content=text,  # raw body, not JSON-encoded
								// headers={"Content-Type": "text/plain"},
								list.Add(new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("headers"),
									Assignment = new DictionaryExpression(new KeyValuePairExpression("Content-Type", "text/plain"))
								});
								if (fromBodyParameter!.Type.IsNullable(compilation)) {
									list.Add(new ScopedVariableExpression {
										Identifier = settings.Async ? new IdentifierNameExpression("content") : new IdentifierNameExpression("data"),
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
									list.Add(new ScopedVariableExpression {
										Identifier = settings.Async ? new IdentifierNameExpression("content") : new IdentifierNameExpression("data"),
										Assignment = new IdentifierNameExpression(fromBodyParameter.Name.Underscore())
									});
								}
							} else {
								// TypeAdapter(datetime).dump_python(datetime.now(), mode="json")
								list.Add(new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("json"),
									Assignment = new InvocationExpression {
										CallableExpression = Defined.Identifiers.TypeAdapter,
										Arguments = {this.typeConverter.Convert(fromBodyParameter.Type) }
									}.Chain(false, new InvocationExpression {
										CallableExpression = new IdentifierNameExpression("dump_python"),
										Arguments = {
											new IdentifierNameExpression(fromBodyParameter.Name.Underscore()),
											new ScopedVariableExpression {
												Identifier = new IdentifierNameExpression("mode"),
												Assignment = new StringLiteralExpression("json")
											}
										}
									})
								});
							}
							return list;
						}
					}, {
						method.HasQueryStringParameter, () => new ScopedVariableExpression {
							Identifier = new IdentifierNameExpression("params"),
							Assignment = new IdentifierNameExpression("params"),
						}
					},
				}
			};
			return new CodeBlock{
				new ScopedVariableExpression {
					Identifier = new IdentifierNameExpression("response"),
					Assignment = expression,
				},
				new InvocationExpression {
					CallableExpression = new MultiPartIdentifierNameExpression("response", "raise_for_status"),
				} };
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) => this.Convert(from);
	}
}