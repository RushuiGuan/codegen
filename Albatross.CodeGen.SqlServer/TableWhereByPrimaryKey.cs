
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableWhereByPrimaryKey : TableQueryGenerator {
		IGetTablePrimaryKey _getPrimaryKey;
		IGetVariableName _getVariableName;

		public override string Name => "table_where_by_primarykey";
		public override string Description => "Where clause by the primary key";

		public TableWhereByPrimaryKey(IGetTablePrimaryKey getPrimaryKey, IGetVariableName getVariableName) {
			_getPrimaryKey = getPrimaryKey;
			_getVariableName = getVariableName;
		}

		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			sb.AppendLine("where");
			IEnumerable<Column> keys = _getPrimaryKey.Get(t);
			if (keys.Count() == 0) {
				throw new PrimaryKeyNotFoundException(t);
			}
			foreach (Column key in keys) {
				string name = _getVariableName.Get(key.Name);
				sb.Tab();
				if (key != keys.First()) { sb.Append("and "); }
				sb.EscapeName(key.Name).Append(" = ").Append(name);
				if (key != keys.Last()) {
					sb.AppendLine();
				}
			}
			return sb;
		}
	}
}
