using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.merge.delete", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement delete clause")]
	public class MergeDelete : TableQueryGenerator {
		public override IEnumerable<object>  Generate(StringBuilder sb, IDictionary<string, string> customCode, Table t, SqlCodeGenOption options) {
			sb.Append("when not matched by source then delete");
			return new[] { this, };
		}
	}
}
