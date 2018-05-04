using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.delete", "sql", Description = "Default delete statement")]
	public class DeleteStatement : TableQueryGenerator {
		public override IEnumerable<object> Build(StringBuilder sb, Table t, SqlCodeGenOption options) {
			sb.Append($"delete from [{t.Schema}].[{t.Name}]");
			return new[] { this };
		}
	}
}
