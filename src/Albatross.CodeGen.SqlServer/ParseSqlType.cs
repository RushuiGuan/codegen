using System;
using System.Text.RegularExpressions;
using Albatross.CodeGen.Database;
using Albatross.Database;

namespace Albatross.CodeGen.SqlServer {
	public class ParseSqlType : IParseSqlType  {
		const string Pattern =
@"^\s* 
	([a-zA-Z_][a-zA-Z0-9_]*) 
	( 
		\((\d+)(\s*,\s*(\d+))?\s*\) 
	)?
\s*$";

		Regex regex = new Regex(Pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);


		public SqlType Parse(string text) {
			Match match = regex.Match(text);
			if (match.Success) {
				SqlType type = new SqlType {
					Name = match.Groups[1].Value.Trim(),
				};
				int? firstParam = null, secondParam = null;
				int i;

				if (int.TryParse(match.Groups[5].Value, out i)) {
					secondParam = i;
				}

				if (int.TryParse(match.Groups[3].Value, out i)) {
					firstParam = i;
					switch (type.Name) {
						case "datetime2":
						case "datetimeoffset":
							type.Precision = null;
							type.Scale = firstParam;
							break;
						case "float":
						case "decimal":
						case "numeric":
							type.Precision = firstParam;
							type.Scale = secondParam;
							break;
						default:
							type.MaxLength = firstParam;
							break;
					}
				}
				return type;
			} else {
				throw new FormatException($"{text} is not a valid sql type");
			}
		}
	}
}
