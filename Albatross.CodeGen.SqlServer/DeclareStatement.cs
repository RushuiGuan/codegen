using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class DeclareStatement : ICodeGenerator<DatabaseObject, SqlQueryOption>{

		IColumnSqlTypeBuilder _typeBuilder;

		public DeclareStatement(IColumnSqlTypeBuilder typeBuilder) {
			_typeBuilder = typeBuilder;
		}

		public string Name => "declare statement";
		public string Category => "Sql server";
		public string Description => "Create a declare statement based on the parameters";
		public string Target => "sql";

		public Type SourceType => typeof(DatabaseObject);
		public Type OptionType => typeof(SqlQueryOption);

		public StringBuilder Build(StringBuilder sb, DatabaseObject list, SqlQueryOption options, ICodeGeneratorFactory factory) {
			sb.AppendLine("declare");


			return sb;
		}

		public StringBuilder Build(StringBuilder sb, object t, object options, ICodeGeneratorFactory factory) {
			return this.Build(sb, (DatabaseObject)t, (SqlQueryOption)options, factory);
		}
	}
}
