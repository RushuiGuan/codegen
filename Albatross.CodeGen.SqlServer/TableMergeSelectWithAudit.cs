using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeSelectWithAudit : TableQueryGenerator {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;
		IBuiltInColumnFactory _builtInColumnFactory;

		public TableMergeSelectWithAudit(IGetTableColumns getColumns, IGetVariableName getVariableName, IBuiltInColumnFactory builtInColumnFactory) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
			_builtInColumnFactory = builtInColumnFactory;
		}

		public override string Name => "table_merge_select_w_audit";
		public override string Description => "Merge statement select clause with audit fields";

		public override StringBuilder Build(StringBuilder sb, Table table, object options, ICodeGeneratorFactory factory) {
			IEnumerable<Column> columns = _getColumns.Get(table);
			if (columns.Count() == 0) {
				throw new ColumnNotFoundException(table);
			}
			sb.Append("using ").OpenParenthesis().AppendLine();
			sb.Tab().AppendLine("select");
			foreach (var column in columns) {
				if ((column.Name == "created" || column.Name == "modified") && _builtInColumnFactory.Match(column)) {
					sb.Tab(2).Append("getdate()");
				} else if ((column.Name == "createdby" || column.Name == "modifiedby") && _builtInColumnFactory.Match(column)) {
					sb.Tab(2).Append("@user");
				} else {
					string name = _getVariableName.Get(column.Name);
					sb.Tab(2).Append(name);
				}
				sb.Append(" as ").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" as src");
			return sb;
		}
	}
}
