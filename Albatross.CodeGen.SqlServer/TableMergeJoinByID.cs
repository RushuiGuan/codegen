using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeJoinByID : TableQueryGenerator {
		IGetTableIdentityColumn _getIDColumn;

		public override string Name => "table_merge_join_by_id";
		public override string Description => "Merge statement source join clause by the identity column";

		public TableMergeJoinByID(IGetTableIdentityColumn getIDColumn) {
			_getIDColumn = getIDColumn;
		}

		public override StringBuilder Build(StringBuilder sb, DatabaseObject table, SqlQueryOption options, ICodeGeneratorFactory factory) {
			Column idColumn = _getIDColumn.Get(table);
			if (idColumn == null) {
				throw new IdentityColumnNotFoundException(table.Schema, table.Name);
			}
			sb.Append("on src.").EscapeName(idColumn.Name).Append(" = dst.").EscapeName(idColumn.Name);
			return sb;
		}
	}
}
