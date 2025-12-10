using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.WebClient.Python {
	public static class Extensions {
		static Regex VariableRegex = new Regex(@"\{\*?(\w+)\}", RegexOptions.Compiled);

		/// <summary>
		/// Give a aspnet route string, convert it to a string interpolation expression
		/// for example: /api/{controller}/{id} => `/api/${controller}/${id}`
		/// /api/{controller}/{*id} => `/api/${controller}/${id}`
		/// </summary>
		/// <param name="route"></param>
		/// <returns></returns>
		public static StringInterpolationExpression ConvertRoute2StringInterpolation(this string route) {
			var matches = VariableRegex.Matches(route);
			if (matches.Count == 0) {
				return new StringInterpolationExpression() {
					Expressions = [new StringLiteralExpression(route)]
				};
			} else {
				List<IExpression> expressions = new List<IExpression>();
				int index = 0;
				foreach (Match match in matches) {
					if (match.Index > index) {
						expressions.Add(new StringLiteralExpression(route.Substring(index, match.Index - index)));
					}
					expressions.Add(new IdentifierNameExpression(match.Groups[1].Value));
					index = match.Index + match.Length;
				}
				if (index < route.Length) {
					expressions.Add(new StringLiteralExpression(route.Substring(index)));
				}
				return new StringInterpolationExpression() {
					Expressions = expressions.ToArray()
				};
			}
		}

		/// <summary>
		/// Parse a string into an IdentifierNameExpression, a fully qualified name can be constructed by using the format name,soure
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static IIdentifierNameExpression ParseIdentifierName(this string name) {
			name = name.Trim();
			var index = name.IndexOf(',');
			var source = string.Empty;
			if (index > 0) {
				source = name.Substring(index + 1).Trim();
				name = name.Substring(0, index).Trim();
			}
			if (string.IsNullOrEmpty(name)) {
				throw new ArgumentException($"{name} is not valid identifier name");
			}
			if (string.IsNullOrEmpty(source)) {
				return new IdentifierNameExpression(name);
			} else {
				return new QualifiedIdentifierNameExpression(name, new ModuleSourceExpression(source));
			}
		}

		public static IServiceCollection AddPythonWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			services.AddSingleton<ITypeConverter, Python.MappedTypeConverter>();
			services.AddSingleton<ISourceLookup>(provider => {
				var settings = provider.GetRequiredService<PythonWebClientSettings>();
				return new DefaultPythonSourceLookup(settings.NamespaceModuleMapping);
			});
			return services;
		}
	}
}