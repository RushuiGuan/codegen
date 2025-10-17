using Humanizer;
using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Text;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using InvocationExpressionBuilder = Albatross.CodeGen.Python.Expressions.InvocationExpressionBuilder;

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

		public PythonFileDeclaration Convert(ControllerInfo model) {
			var fileName = $"{model.ControllerName.ToLowerInvariant()}_client";
			return new PythonFileDeclaration(fileName) {
				ClasseDeclarations = [
					new ClassDeclaration($"{model.ControllerName}Client") {
						Fields = [
							new FieldDeclaration("endPoint") {
								Type = Defined.Types.String,
							}
						],
						Constructor = new ConstructorDeclaration() {
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
									new ScopedVariableExpression{
										Identifier =new MultiPartIdentifierNameExpression("self", "base_url"),
										Assignment = new InvocationExpression {
											Identifier = new MultiPartIdentifierNameExpression("base_url", "rstrip"),
											ArgumentList = new ListOfSyntaxNodes<IExpression>(new StringLiteralExpression("/")),
										},
									},
									
								]
							},
						},
						Methods = GroupMethods(model).Select(x=> BuildMethod(x.Method, x.Index)).ToArray(),
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
			var name = index == 0 ? method.Name.Underscore() : $"{method.Name.Underscore()}{index}";
			return new MethodDeclaration(name) {
				Modifiers = settings.UsePromise ? [new AsyncModifier()] : [],
				ReturnType = settings.UsePromise ? returnType.ToPromise() : returnType.ToObservable(),
				Parameters = new ListOfSyntaxNodes<ParameterDeclaration>(method.Parameters.Select(x => new ParameterDeclaration(x.Name) { Type = typeConverter.Convert(x.Type) })),
				Body = new ScopedVariableExpressionBuilder()
					.IsConstant()
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
				Identifier = new QualifiedIdentifierNameExpression("format", Defined.Sources.DateFns),
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

		JsonValueExpression BuildQueryStringParameters(MethodInfo method) {
			var properties = new List<JsonPropertyExpression>();
			foreach (var param in method.Parameters.Where(x => x.WebType == ParameterType.FromQuery)) {
				properties.Add(BuildQueryStringParameter(param, method.Settings.UseDateTimeAsDateOnly == true));
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
		JsonPropertyExpression BuildQueryStringParameter(ParameterInfo parameter, bool useDateTimeAsDateOnly) {
			IExpression value;
			if (parameter.Type.TryGetCollectionElementType(out var elementType) && IsDate(elementType!)) {
				value = new InvocationExpression {
					Identifier = new MultiPartIdentifierNameExpression(parameter.Name.CamelCase(), "map"),
					ArgumentList = new ListOfSyntaxNodes<IExpression>(
						new ArrowFunctionExpression {
							Arguments = new ListOfSyntaxNodes<IIdentifierNameExpression>(new IdentifierNameExpression("x")),
							Body = BuildParamValue("x", elementType!, useDateTimeAsDateOnly),
						}
					)
				};
			} else {
				value = BuildParamValue(parameter.Name.CamelCase(), parameter.Type, useDateTimeAsDateOnly);
			}
			return new JsonPropertyExpression(parameter.QueryKey, value);
		}

		IExpression CreateHttpInvocationExpression(MethodInfo method) {
			var builder = new InvocationExpressionBuilder();
			var returnType = this.typeConverter.Convert(method.ReturnType);
			if (object.Equals(returnType, Defined.Types.Void())) {
				returnType = Defined.Types.Object();
			}
			var hasStringReturnType = object.Equals(returnType, Defined.Types.String());
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
				builder.AddGenericArgument(Defined.Types.String());
				builder.AddArgument(new StringLiteralExpression(""));
			}

			// build query string
			builder.AddArgument(BuildQueryStringParameters(method));

			if (settings.UsePromise) {
				return new ScopedVariableExpressionBuilder()
					.IsConstant().WithName("result").WithExpression(builder.Build())
					.Add(() => new ReturnExpression(new InvocationExpression() {
						Identifier = Defined.Identifiers.FirstValueFrom,
						ArgumentList = new ListOfSyntaxNodes<IExpression>(new IdentifierNameExpression("result")),
						UseAwaitOperator = true,
					}))
					.BuildAll();
			} else {
				return new ScopedVariableExpressionBuilder()
					.IsConstant().WithName("result").WithExpression(builder.Build())
					.Add(() => new ReturnExpression(new IdentifierNameExpression("result")))
					.BuildAll();
			}
		}

		object IConvertObject<ControllerInfo>.Convert(ControllerInfo from) => this.Convert(from);
	}
}