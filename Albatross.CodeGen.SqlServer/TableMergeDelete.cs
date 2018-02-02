using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_merge_delete", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merage statement delete clause")]
	public class TableMergeDelete : TableQueryGenerator {
		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption options, ICodeGeneratorFactory factory, out IEnumerable<object> used) {
			used = new[] { this };
			return sb.Append("when not matched by source then delete");
		}
	}
}
