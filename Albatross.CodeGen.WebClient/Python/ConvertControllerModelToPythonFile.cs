﻿using Humanizer;
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
		public const string ControllerPostfix = "Controller";
		public const string ControllerNamePlaceholder = "[controller]";
		private readonly PythonWebClientSettings settings;
		private readonly IConvertObject<ITypeSymbol, ITypeExpression> typeConverter;

		public ConvertControllerModelToPythonFile(CodeGenSettings settings, IConvertObject<ITypeSymbol, ITypeExpression> typeConverter) {
			this.settings = settings.PythonWebClientSettings;
			this.typeConverter = typeConverter;
		}

		private static readonly IIdentifierNameExpression AsyncClient = new QualifiedIdentifierNameExpression("AsyncClient", new ModuleSourceExpression("httpx"));
		private static readonly IIdentifierNameExpression HttpNtlmAuth = new QualifiedIdentifierNameExpression("HttpNtlmAuth", new ModuleSourceExpression("httpx_ntlm"));

		public PythonFileDeclaration Convert(ControllerInfo model) {
			var fileName = $"{model.ControllerName.Underscore()}_client";
			return new PythonFileDeclaration(fileName) {
				ClasseDeclarations = [
					new ClassDeclaration($"{model.ControllerName}Client") {
						Fields = [
							new FieldDeclaration("base_url") {
								Type = Defined.Types.String,
							},
							new FieldDeclaration("_client") {
								Type = new SimpleTypeExpression {
									Identifier = AsyncClient
								}
							},
						],
						Constructor = CreateConstructor(),
						Methods = new MethodDeclaration[]{
							CreateCloseMethod(),
							CreateAsyncEnterMethod(),
							CreateAsyncExitMethod()
							
						}.Concat(GroupMethods(model).Select(x => BuildMethod(x.Method, x.Index))).ToArray(),
					}
				],
			};
		}

		MethodDeclaration CreateConstructor() => new ConstructorDeclaration() {
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration> {
				Nodes = [
					Defined.Parameters.Self,
					new ParameterDeclaration {
						Identifier = new IdentifierNameExpression("base_url"),
						Type = Defined.Types.String
					},
				],
			},
			Body = new CompositeExpression {
				Items = [
					new ScopedVariableExpression {
						Identifier = new MultiPartIdentifierNameExpression("self", "base_url"),
						Assignment = new InvocationExpression {
							Identifier = new MultiPartIdentifierNameExpression("base_url", "rstrip"),
							ArgumentList = new ListOfSyntaxNodes<IExpression>(new StringLiteralExpression("/")),
						},
					},
					new ScopedVariableExpression {
						Identifier = new MultiPartIdentifierNameExpression("self", "_client"),
						Assignment = new InvocationExpression {
							Identifier = new QualifiedIdentifierNameExpression("AsyncClient", new ModuleSourceExpression("httpx")),
							ArgumentList = new ListOfSyntaxNodes<IExpression>(
								new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("base_url"),
									Assignment = new MultiPartIdentifierNameExpression("self", "base_url"),
								},
								new ScopedVariableExpression {
									Identifier = new IdentifierNameExpression("auth"),
									Assignment = new InvocationExpression {
										Identifier = HttpNtlmAuth,
										ArgumentList = new ListOfSyntaxNodes<IExpression>(
											Defined.Types.None, Defined.Types.None
										),
									},
								}
							),
						}
					}
				]
			},
		};
		MethodDeclaration CreateCloseMethod() => new MethodDeclaration("close") {
			Modifiers = [new AsyncModifier()],
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(
				new ParameterDeclaration{
					Identifier = Defined.Identifiers.Self,
				}),
			ReturnType = Defined.Types.None,
			Body = new InvocationExpressionBuilder()
				.Await()
				.WithMultiPartName("self", "_client", "aclose")
				.Build(),
		};
		MethodDeclaration CreateAsyncEnterMethod() => new MethodDeclaration("__aenter__") {
			Modifiers = [new AsyncModifier()],
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(
				new ParameterDeclaration{
					Identifier = Defined.Identifiers.Self,
				}),
			ReturnType = Defined.Types.None,
			Body = new ReturnExpression(Defined.Identifiers.Self),
		};
		MethodDeclaration CreateAsyncExitMethod() => new MethodDeclaration("__aexit__") {
			Modifiers = [new AsyncModifier()],
			Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(
				new ParameterDeclaration{
					Identifier = Defined.Identifiers.Self,
				}),
			ReturnType = Defined.Types.None,
			Body = new InvocationExpressionBuilder().Await().WithMultiPartName("self", "close").Build(),
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
				Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(method.Parameters.Select(x => new ParameterDeclaration { Identifier = new IdentifierNameExpression(x.Name), Type = typeConverter.Convert(x.Type) })),
				Body = new ScopedVariableExpressionBuilder()
					.WithName("relativeUrl").WithExpression(new StringInterpolationExpression(method.RouteSegments.Select(x => BuildRouteSegment(method, x))))
					.Add(() => CreateHttpInvocationExpression(method))
					.BuildAll()
			};
		}

		const string TimeOnlyFormat = "HH:mm:ss.SSS";
		const string DateOnlyFormat = "yyyy-MM-dd";
		const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ssXXX";

		IExpression BuildRouteSegment(MethodInfo method, IRouteSegment segment) {
			if (segment is RouteParameterSegment parameterSegment) {
				return BuildParamValue(segment.Text, parameterSegment.RequiredParameterInfo.Type, method.Settings.UseDateTimeAsDateOnly == true);
			} else {
				return new StringLiteralExpression(segment.Text);
			}
		}

		InvocationExpression FormattedDate(string text, string format) {
			return new InvocationExpression {
				Identifier = new IdentifierNameExpression("format"),
				ArgumentList = new ListOfSyntaxNodes<IExpression>(
					new IdentifierNameExpression(text),
					new StringLiteralExpression(format)),
			};
		}

		IExpression BuildParamValue(string variableName, ITypeSymbol elementType, bool useDateTimeAsDateOnly) {
			IExpression value;
			var typeName = elementType!.GetFullName();
			if (typeName == typeof(TimeOnly).FullName) {
				value = FormattedDate(variableName, TimeOnlyFormat);
			} else if (typeName == typeof(DateOnly).FullName) {
				value = FormattedDate(variableName, DateOnlyFormat);
			} else if (typeName == typeof(DateTime).FullName || typeName == typeof(DateTimeOffset).FullName) {
				if (useDateTimeAsDateOnly) {
					value = FormattedDate(variableName, DateOnlyFormat);
				} else {
					value = FormattedDate(variableName, DateTimeFormat);
				}
			} else {
				value = new IdentifierNameExpression(variableName);
			}
			return value;
		}

		DictionaryValueExpression BuildQueryStringParameters(MethodInfo method) {
			var properties = new List<KeyValuePairExpression>();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				properties.Add(BuildQueryStringParameter(param, method.Settings.UseDateTimeAsDateOnly == true));
			}
			return new DictionaryValueExpression(properties);
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
		KeyValuePairExpression BuildQueryStringParameter(ParameterInfo parameter, bool useDateTimeAsDateOnly) {
			IExpression value;
			if (parameter.Type.TryGetCollectionElementType(out var elementType) && IsDate(elementType!)) {
				value = new InvocationExpression {
					Identifier = new MultiPartIdentifierNameExpression(parameter.Name.CamelCase(), "map"),
					ArgumentList = new ListOfSyntaxNodes<IExpression>()
				};
			} else {
				value = BuildParamValue(parameter.Name.CamelCase(), parameter.Type, useDateTimeAsDateOnly);
			}
			return new KeyValuePairExpression(new StringLiteralExpression(parameter.QueryKey), value);
		}

		IExpression CreateHttpInvocationExpression(MethodInfo method) {
			var builder = new InvocationExpressionBuilder();
			var returnType = this.typeConverter.Convert(method.ReturnType);
			var hasStringReturnType = object.Equals(returnType, Defined.Types.String);
			switch (method.HttpMethod) {
				case My.HttpMethodGet:
					if (hasStringReturnType) {
						builder.WithMultiPartName("this", "doGetStringAsync");
					} else {
						builder.WithMultiPartName("this", "doGetAsync").AddGenericArgument(returnType);
					}
					break;
				case My.HttpMethodPost:
					if (hasStringReturnType) {
						builder.WithMultiPartName("this", "doPostStringAsync");
					} else {
						builder.WithMultiPartName("this", "doPostAsync");
						builder.AddGenericArgument(returnType);
					}
					break;
				case My.HttpMethodPatch:
					if (hasStringReturnType) {
						builder.WithMultiPartName("this", "doPatchStringAsync");
					} else {
						builder.WithMultiPartName("this", "doPatchAsync");
						builder.AddGenericArgument(returnType);
					}
					break;
				case My.HttpMethodPut:
					if (hasStringReturnType) {
						builder.WithMultiPartName("this", "doPutStringAsync");
					} else {
						builder.WithMultiPartName("this", "doPutAsync");
						builder.AddGenericArgument(returnType);
					}
					break;
				case My.HttpMethodDelete:
					builder.WithMultiPartName("this", "doDeleteAsync");
					break;
			}
			// add relativeUrl parameter
			builder.AddArgument(new IdentifierNameExpression("relativeUrl"));
			// add from body parameter if it exists
			var fromBodyParameter = method.Parameters.FirstOrDefault(x => x.WebType == ParameterType.FromBody);
			if (fromBodyParameter != null) {
				builder.AddGenericArgument(this.typeConverter.Convert(fromBodyParameter.Type));
				builder.AddArgument(new IdentifierNameExpression(fromBodyParameter.Name.CamelCase()));
			} else if (method.HttpMethod == My.HttpMethodPost || method.HttpMethod == My.HttpMethodPut || method.HttpMethod == My.HttpMethodPatch) {
				builder.AddGenericArgument(Defined.Types.String);
				builder.AddArgument(new StringLiteralExpression(""));
			}
			// build query string
			builder.AddArgument(BuildQueryStringParameters(method));
			return new ScopedVariableExpressionBuilder()
				.WithName("result").WithExpression(builder.Build())
				.Add(() => new ReturnExpression(new InvocationExpression() {
					Identifier = new IdentifierNameExpression("xx"),
					ArgumentList = new ListOfSyntaxNodes<IExpression>(new IdentifierNameExpression("result")),
					UseAwaitOperator = true,
				}))
				.BuildAll();
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) => this.Convert(from);
	}
}