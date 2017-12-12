using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class BuildDeclareStatement : ICodeGenerator{
		ISqlValueCollection _values;
		public BuildDeclareStatement(ISqlValueCollection values) {
			_values = values;
		}

		public string Name => "declare statement";
		public string Cateogry => "Sql server";
		public string Description => "Create a declare statement based on the parameters";

		public StringBuilder Build(StringBuilder sb) {
			sb.AppendLine("declare");
			foreach (var pair in _values) {
				sb.Tab().Append(pair.Key).Append(" as ");
				_typeBuilder.Build(sb, pair.Value);
				if (pair.Key != @params.Keys.Last()) {
					sb.Comma().AppendLine();
				} else {
					sb.Semicolon();
				}
			}
			return sb;
		}
	}
}
