using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeInsert : TableQueryGenerator {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;

		public TableMergeInsert(IGetTableColumns getColumns, IGetVariableName getVariableName) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
		}

		public override string Description => "Merge statement insert clause";
		public override string Name => "table_merge_insert";

		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			Column[] columns = (from c in _getColumns.Get(t)
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.ComputedColumn && !c.IdentityColumn
								select c).ToArray();
			if (columns.Length == 0) {
				throw new ColumnNotFoundException(t.Schema, t.Name);
			}
			sb.Append("when not matched by target then insert ").OpenParenthesis().AppendLine();
			foreach (var column in columns) {
				sb.Tab().EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" values ").OpenParenthesis().AppendLine();
			foreach (var column in columns) {
				sb.Tab().Append("src.").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis();
			return sb;
		}
	}
}
