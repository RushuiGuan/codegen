using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeAnalysis.Syntax;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.CSharp {
	public class ConvertWebApiToCSharpFile : IConvertObject<ControllerInfo, FileDeclaration> {
		const string ProxyService = "ProxyService";
		private readonly Compilation compilation;
		private readonly CSharpWebClientSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertWebApiToCSharpFile(Compilation compilation, CSharpWebClientSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.compilation = compilation;
			this.settings = settings;
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
			return new ListOfParameterDeclarations(method.Parameters.Select(param => new ParameterDeclaration {
				Name = new IdentifierNameExpression(param.Name),
				Type = typeConverter.Convert(param.Type),
			}));
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
				BaseTypes = new ITypeExpression[] { new TypeExpression("ClientBase") }
					.Concat(settings.UseInterface ? [new TypeExpression(GetInterfaceName(controller))] : []),
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
				],
				Methods = from method in controller.Methods select CreateMethod(method),
			};
		}

		string GetInterfaceName(ControllerInfo controller) {
			return $"I{GetClassName(controller)}";
		}

		string GetClassName(ControllerInfo controller) {
			return $"{controller.ControllerName}{ProxyService}";
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
				Body = new CodeBlock {
					BuildPath(method),
					{ true,() => BuildQuery(method) },
					{ true,() => BuildHttpCall(method) },
				},
			};
		}

		IExpression BuildPath(MethodInfo method) {
			return new AssignmentExpression {
				Left = new VariableDeclaration {
					Type = Defined.Types.String,
					Identifier = new IdentifierNameExpression("path"),
				},
				Expression = new StringInterpolationExpression {
					Expressions = BuildRouteSegments(method.RouteSegments).ToArray(),
				}
			}.EndOfStatement();
		}

		IEnumerable<IExpression> BuildQuery(MethodInfo method) {
			yield return new AssignmentExpression {
				Left = new VariableDeclaration {
					Type = Defined.Types.Var,
					Identifier = new IdentifierNameExpression("queryString"),
				},
				Expression = new NewObjectExpression {
					Type = Defined.Types.NameValueCollection,
				}
			}.EndOfStatement();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				if (param.Type.TryGetCollectionElementType(compilation, out var elementType)) {
					yield return new ForEachExpression {
						IterationVariable = new VariableDeclaration {
							Identifier = new IdentifierNameExpression("item"),
						},
						Collection = new IdentifierNameExpression(param.Name),
						Body = CreateAddQueryStringStatement(elementType, param.QueryKey, "item")
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
					Parameters = new(
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("logger"),
							Type = new TypeExpression("ILogger", GetClassName(from)),
						},
						new ParameterDeclaration {
							Name = new IdentifierNameExpression("client"),
							Type = new TypeExpression("HttpClient"),
						}
					),
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

		ListOfArguments CreateRequestFunctionArguments(MethodInfo method, ParameterInfo? fromBody) {
			var list = new List<IExpression> {
				new IdentifierNameExpression($"HttpMethod.{method.HttpMethod}"),
				new IdentifierNameExpression("path"),
				new IdentifierNameExpression("queryString"),
			};
			if (fromBody != null) {
				list.Add(new IdentifierNameExpression(fromBody.Name));
			}
			return new ListOfArguments(list);
		}

		IExpression CreateRequestFunction(ParameterInfo? fromBody) {
			if (fromBody == null) {
				return new IdentifierNameExpression("this.CreateRequest");
			} else if (fromBody.Type.SpecialType == SpecialType.System_String) {
				return new IdentifierNameExpression("this.CreateStringRequest");
			} else {
				return new IdentifierNameExpression("this.CreateJsonRequest") {
					GenericArguments = new ListOfGenericArguments(this.typeConverter.Convert(fromBody.Type))
				};
			}
		}

		IExpression GetVoidResponseFunction() {
			return new InvocationExpression {
				CallableExpression = new IdentifierNameExpression("this.GetRawResponse"),
				Arguments = new ListOfArguments(new IdentifierNameExpression("request")),
				UseAwaitOperator = true,
			};
		}

		IExpression GetStringResponseFunction() {
			return new ReturnExpression {
				Expression = GetVoidResponseFunction(),
			};
		}

		IExpression GetJsonResponseFunction(MethodInfo method) {
			string functionName;
			if (method.ReturnType.IsNullable(compilation)) {
				functionName = "this.GetJsonResponse";
			} else if (method.ReturnType.IsValueType) {
				functionName = "this.GetRequiredJsonResponseForValueType";
			} else {
				functionName = "this.GetRequiredJsonResponse";
			}
			return new ReturnExpression {
				Expression = new InvocationExpression {
					CallableExpression = new IdentifierNameExpression(functionName) {
						GenericArguments = new ListOfGenericArguments(this.typeConverter.Convert(method.ReturnType))
					},
					Arguments = new ListOfArguments(new IdentifierNameExpression("request")),
					UseAwaitOperator = true,
				}
			};
		}

		IExpression GetResponseFunction(MethodInfo method) {
			if (method.ReturnType.SpecialType == SpecialType.System_Void) {
				return GetVoidResponseFunction().EndOfStatement();
			} else if (method.ReturnType.SpecialType == SpecialType.System_String) {
				return GetStringResponseFunction();
			} else {
				return GetJsonResponseFunction(method);
			}
		}

		IEnumerable<IExpression> BuildHttpCall(MethodInfo method) {
			var fromBody = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
			yield return new UsingExpression {
				Resource = new AssignmentExpression {
					Left = new VariableDeclaration {
						Type = Defined.Types.Var,
						Identifier = new IdentifierNameExpression("request"),
					},
					Expression = new InvocationExpression {
						CallableExpression = CreateRequestFunction(fromBody),
						Arguments = CreateRequestFunctionArguments(method, fromBody)
					}
				},
				Body = GetResponseFunction(method)
			};
		}

		public FileDeclaration Convert(ControllerInfo from) {
			return new FileDeclaration(GetFilename(from)) {
				Namespace = new NamespaceExpression(settings.Namespace),
				NullableEnabled = true,
				Imports = [
					new ImportExpression("Albatross.Dates"),
					new ImportExpression("System.Net.Http"),
					new ImportExpression("Microsoft.Extensions.Logging"),
					new ImportExpression("Albatross.WebClient"),
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
							CallableExpression = new IdentifierNameExpression($"{routeParam.Text}.ISO8601String"),
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
			} else if (type.TryGetNullableValueType(compilation, out var valueType) && IsDate(valueType)) {
				return new InvocationExpression {
					CallableExpression = new IdentifierNameExpression($"{variableName}.Value.ISO8601String"),
				};
			} else {
				return new StringInterpolationExpression {
					Expressions = [new IdentifierNameExpression(variableName)]
				};
			}
		}

		IExpression CreateAddQueryStringStatement(ITypeSymbol type, string queryKey, string variableName) {
			if (type.IsNullable(compilation)) {
				return new IfExpression {
					Condition = new InfixExpression {
						Left = new IdentifierNameExpression(variableName),
						Operator = Defined.Operators.NotEqual,
						Right = new NullExpression()
					},
					IfBlock = new InvocationExpression {
						CallableExpression = new IdentifierNameExpression("queryString.Add"),
						Arguments = new ListOfArguments(new StringLiteralExpression(queryKey), GetQueryStringValue(type, variableName))
					}.EndOfStatement()
				};
			} else {
				return new InvocationExpression {
					CallableExpression = new IdentifierNameExpression("queryString.Add"),
					Arguments = new ListOfArguments(new StringLiteralExpression(queryKey), GetQueryStringValue(type, variableName))
				}.EndOfStatement();
			}
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) {
			return Convert(from);
		}
	}
}