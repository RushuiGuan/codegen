using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.TypeScript.Declarations;
using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.TypeScript.Modifiers;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Text;
using Humanizer;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.WebClient.TypeScript {
	public class ConvertControllerModelToTypeScriptFile : IConvertObject<ControllerInfo, TypeScriptFileDeclaration> {
		public const string ControllerPostfix = "Controller";
		public const string ControllerNamePlaceholder = "[controller]";
		private readonly TypeScriptWebClientSettings settings;
		private readonly Compilation compilation;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertControllerModelToTypeScriptFile(Compilation compilation, TypeScriptWebClientSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.settings = settings;
			this.compilation = compilation;
			this.typeConverter = typeConverter;
		}

		public TypeScriptFileDeclaration Convert(ControllerInfo model) {
			var fileName = $"{model.ControllerName.Kebaberize()}.service.generated";
			return new TypeScriptFileDeclaration(fileName) {
				ClasseDeclarations = [
					new ClassDeclaration($"{model.ControllerName}Service") {
						Decorators = [
							Defined.Invocations.InjectableDecorator("root"),
						],
						BaseClassName = new QualifiedIdentifierNameExpression(settings.BaseClassName, new ModuleSourceExpression(settings.BaseClassModule)),
						Getters = [
							new GetterDeclaration("endPoint") {
								ReturnType = Defined.Types.String(),
								Body = new ReturnExpression(new InfixExpression {
									Operator = new Operator("+"),
									Left = new InvocationExpression {
										CallableExpression = new MultiPartIdentifierNameExpression("this", "config", "endpoint"),
										Arguments = new ListOfSyntaxNodes<IExpression> { new StringLiteralExpression(settings.EndPointName) },
									},
									Right = new StringLiteralExpression(model.Route),
								}),
							}
						],
						Constructor = new ConstructorDeclaration() {
							Parameters = new ListOfSyntaxNodes<ParameterDeclaration> {
								new ParameterDeclaration("config") {
									Type = new SimpleTypeExpression {
										Identifier = new QualifiedIdentifierNameExpression("ConfigService", new ModuleSourceExpression(settings.ConfigServiceModule)),
									},
									Modifiers = [Defined.Keywords.Private],
								},
								new ParameterDeclaration("client") {
									Type = Defined.Types.HttpClient(),
									Modifiers = [Defined.Keywords.Protected],
								}
							},
							Body = new TypeScriptCodeBlock {
								new InvocationExpression {
									CallableExpression = new IdentifierNameExpression("super"),
								},
								Defined.Invocations.ConsoleLog($"{model.ControllerName}Service instance created"),
							},
						},
						Methods = GroupMethods(model).Select(x => BuildMethod(x.Method, x.Index)).ToArray(),
					}
				],
			};
		}

		// has to do this since typescript doesn't support methods of the same name
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
			if (object.Equals(returnType, Defined.Types.Void())) {
				returnType = Defined.Types.Object();
			}
			var name = index == 0 ? method.Name.CamelCase() : $"{method.Name.CamelCase()}{index}";
			return new MethodDeclaration(name) {
				Modifiers = settings.UsePromise ? [new AsyncKeyword()] : [],
				ReturnType = settings.UsePromise ? returnType.ToPromise() : returnType.ToObservable(),
				Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(method.Parameters.Select(x => new ParameterDeclaration(x.Name) { Type = typeConverter.Convert(x.Type) })),
				Body = new CodeBlock(
					new ScopedVariableExpression("relativeUrl") {
						IsConstant = true,
						Assignment = new StringInterpolationExpression(method.RouteSegments.Select(x => BuildRouteSegment(method, x)))
					},
					CreateHttpInvocationExpression(method)
				),
			};
		}

		const string TimeOnlyFormat = "HH:mm:ss.SSS";
		const string DateOnlyFormat = "yyyy-MM-dd";
		const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ssXXX";

		IExpression BuildRouteSegment(MethodInfo method, IRouteSegment segment) {
			if (segment is RouteParameterSegment parameterSegment) {
				return BuildParamValue(segment.Text, parameterSegment.RequiredParameterInfo.Type);
			} else {
				return new StringLiteralExpression(segment.Text);
			}
		}

		InvocationExpression FormattedDate(string text, string format) {
			return new InvocationExpression {
				CallableExpression = new QualifiedIdentifierNameExpression("format", Defined.Sources.DateFns),
				Arguments = new ListOfSyntaxNodes<IExpression>(
					new IdentifierNameExpression(text),
					new StringLiteralExpression(format)),
			};
		}

		IExpression BuildParamValue(string variableName, ITypeSymbol elementType) {
			IExpression value;
			var typeName = elementType!.GetFullName();
			if (typeName == typeof(TimeOnly).FullName) {
				value = FormattedDate(variableName, TimeOnlyFormat);
			} else if (typeName == typeof(DateOnly).FullName) {
				value = FormattedDate(variableName, DateOnlyFormat);
			} else if (typeName == typeof(DateTime).FullName || typeName == typeof(DateTimeOffset).FullName) {
				value = FormattedDate(variableName, DateTimeFormat);
			} else {
				value = new IdentifierNameExpression(variableName);
			}
			return value;
		}

		JsonValueExpression BuildQueryStringParameters(MethodInfo method) {
			var properties = new List<JsonPropertyExpression>();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				properties.Add(BuildQueryStringParameter(param));
			}
			return new JsonValueExpression(properties);
		}

		bool IsDate(ITypeSymbol type) {
			var typeName = type.GetFullName();
			return typeName == typeof(TimeOnly).FullName
			       || typeName == typeof(DateOnly).FullName
			       || typeName == typeof(DateTime).FullName
			       || typeName == typeof(DateTimeOffset).FullName;
		}

		/// <summary>
		/// This method will generate this
		/// { dates:dates.map(x=>format(x, "yyyy-MM-dd")) }
		/// { d:dates.map(x=>format(x, "yyyy-MM-dd")) }
		/// { value }
		/// { v: value }
		/// </summary>
		JsonPropertyExpression BuildQueryStringParameter(ParameterInfo parameter) {
			IExpression value;
			if (parameter.Type.TryGetCollectionElementType(compilation, out var elementType) && IsDate(elementType!)) {
				value = new InvocationExpression {
					CallableExpression = new MultiPartIdentifierNameExpression(parameter.Name.CamelCase(), "map"),
					Arguments = new ListOfSyntaxNodes<IExpression>(
						new ArrowFunctionExpression {
							Arguments = new ListOfSyntaxNodes<IIdentifierNameExpression>(new IdentifierNameExpression("x")),
							Body = BuildParamValue("x", elementType!),
						}
					)
				};
			} else {
				value = BuildParamValue(parameter.Name.CamelCase(), parameter.Type);
			}
			return new JsonPropertyExpression(parameter.QueryKey, value);
		}

		IExpression CreateHttpRequestInvocationExpression(MethodInfo method) {
			var fromBodyParameter = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
			var fromBodyParameterType = fromBodyParameter?.Type;
			if (fromBodyParameterType == null && HasRequestBody(method.HttpMethod)) {
				fromBodyParameterType = compilation.GetSpecialType(SpecialType.System_String);
			}
			var hasFromBodyParameter = fromBodyParameter != null;
			var returnType = this.typeConverter.Convert(method.ReturnType);
			if (object.Equals(returnType, Defined.Types.Void())) {
				returnType = Defined.Types.Object();
			}
			var hasStringReturnType = object.Equals(returnType, Defined.Types.String());

			return new InvocationExpression {
				CallableExpression = new MultiPartIdentifierNameExpression("this", GetHttpRquestMethodIdentifier(method.HttpMethod, hasStringReturnType)) {
					GenericArguments = new ListOfGenericArguments {
						{ !hasStringReturnType && method.HttpMethod != "Delete", () => returnType },
						{ fromBodyParameterType != null, () => typeConverter.Convert(fromBodyParameterType!) }
					},
				},
				Arguments = new ListOfSyntaxNodes<IExpression> {
					new IdentifierNameExpression("relativeUrl"), {
						HasRequestBody(method.HttpMethod), () => {
							if (hasFromBodyParameter) {
								return new IdentifierNameExpression(fromBodyParameter!.Name.CamelCase());
							} else {
								return new StringLiteralExpression("");
							}
						}
					},
					BuildQueryStringParameters(method)
				},
			};
		}

		IExpression CreateHttpInvocationExpression(MethodInfo method) {
			if (settings.UsePromise) {
				return new CodeBlock(
					new ScopedVariableExpression("result") {
						IsConstant = true,
						Assignment = CreateHttpRequestInvocationExpression(method),
					},
					new ReturnExpression(new InvocationExpression {
						CallableExpression = Defined.Identifiers.FirstValueFrom,
						Arguments = new ListOfSyntaxNodes<IExpression>(new IdentifierNameExpression("result")),
						UseAwaitOperator = true,
					}));
			} else {
				return new CodeBlock(
					new ScopedVariableExpression("result") {
						Assignment = CreateHttpRequestInvocationExpression(method),
						IsConstant = true,
					},
					new ReturnExpression(new IdentifierNameExpression("result")));
			}
		}

		bool HasRequestBody(string httpMethod) {
			return httpMethod == "Post" || httpMethod == "Put" || httpMethod == "Patch";
		}

		string GetHttpRquestMethodIdentifier(string method, bool hasStringReturnType) {
			return method switch {
				"Get" => hasStringReturnType ? "doGetStringAsync" : "doGetAsync",
				"Post" => hasStringReturnType ? "doPostStringAsync" : "doPostAsync",
				"Put" => hasStringReturnType ? "doPutStringAsync" : "doPutAsync",
				"Patch" => hasStringReturnType ? "doPatchStringAsync" : "doPatchAsync",
				"Delete" => "doDeleteAsync",
				_ => throw new NotSupportedException($"HTTP method {method} is not supported"),
			};
		}


		object IConvertObject<ControllerInfo>.
			Convert(ControllerInfo from) => this.Convert(from);
	}
}