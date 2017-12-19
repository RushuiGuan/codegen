
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class TableMergeJoinByPrimaryKey : TableCodeGenerator {
		IGetTablePrimaryKey _getPrimaryKey;

		public override string Name => "table_merge_join_by_primarykey";
		public override string Description => "Merge statement source join clause by the primary key columns";

		public TableMergeJoinByPrimaryKey(IGetTablePrimaryKey getPrimaryKey) {
			_getPrimaryKey = getPrimaryKey;
		}

		public override StringBuilder Build(StringBuilder sb, Table table, ICodeGeneratorFactory factory) {
			IEnumerable<Column> keys = _getPrimaryKey.Get(table);
			if (keys.Count() == 0) {
				throw new PrimaryKeyNotFoundException(table.Schema, table.Name);
			}
			sb.Append("on ");
			foreach (Column key in keys) {
				if (key != keys.First()) { sb.AppendLine().Tab().Append("and "); }
				sb.Append("src.").EscapeName(key.Name).Append(" = dst.").EscapeName(key.Name);
			}
			return sb;
		}
	}
}
