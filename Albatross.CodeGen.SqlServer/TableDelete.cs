using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_delete", "sql", Description = "Default delete statement")]
	public class TableDelete : TableQueryGenerator {
		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			return sb.Append($"delete from [{t.Schema}].[{t.Name}]");
		}
	}
}
