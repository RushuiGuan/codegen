using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class BuildDeclareStatement : ICodeGenerator<ParamCollection, SqlQueryOption>{

		IColumnSqlTypeBuilder _typeBuilder;

		public BuildDeclareStatement(IColumnSqlTypeBuilder typeBuilder) {
			_typeBuilder = typeBuilder;
		}

		public string Name => "declare statement";
		public string Category => "Sql server";
		public string Description => "Create a declare statement based on the parameters";
		public string Target => "sql";

		public Type SourceType => typeof(ParamCollection);
		public Type OptionType => typeof(object);

		public StringBuilder Build(StringBuilder sb, ParamCollection list, SqlQueryOption options, ICodeGeneratorFactory factory) {
			sb.AppendLine("declare");
			foreach (var pair in list) {
				sb.Tab().Append(pair.Key).Append(" as ");
				sb.Append(_typeBuilder.Build(pair.Value));
				if (pair.Key != list.Keys.Last()) {
					sb.Comma().AppendLine();
				} else {
					sb.Semicolon();
				}
			}
			return sb;
		}

		public StringBuilder Build(StringBuilder sb, object t, object options, ICodeGeneratorFactory factory) {
			return this.Build(sb, (ParamCollection)t, (SqlQueryOption)options, factory);
		}
	}
}
