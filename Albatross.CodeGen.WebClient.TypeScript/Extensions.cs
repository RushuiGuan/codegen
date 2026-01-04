using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.WebClient.TypeScript {
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

		public static IServiceCollection AddTypeScriptWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			services.AddSingleton<ITypeConverter, TypeScript.MappedTypeConverter>();
			services.AddSingleton<ISourceLookup>(provider => {
				var settings = provider.GetRequiredService<ICodeGenSettingsFactory>().Get<TypeScriptWebClientSettings>();
				return new DefaultTypeScriptSourceLookup(settings.NamespaceModuleMapping);
			});
			return services;
		}
	}
}