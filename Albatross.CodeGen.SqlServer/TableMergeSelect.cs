
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
	public class TableMergeSelect : TableQueryGenerator {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;
		IColumnSqlTypeBuilder typeBuilder;

		public TableMergeSelect(IGetTableColumns getColumns, IGetVariableName getVariableName, IColumnSqlTypeBuilder typeBuilder) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
			this.typeBuilder = typeBuilder;
		}

		public override string Name  => "table_merge_select";
		public override string Description => "Merge statement select clause";

		public override StringBuilder Build(StringBuilder sb, Table table, SqlQueryOption options, ICodeGeneratorFactory factory) {
			Column[] columns = _getColumns.Get(table).ToArray();
			if (columns.Length == 0) {
				throw new ColumnNotFoundException(table);
			}
			sb.Append("using ").OpenParenthesis().AppendLine();
			sb.Tab().AppendLine("select");
			foreach (var column in columns) {
				sb.Tab(2);
				if (options.Expressions.TryGetValue(column.Name, out string expression)) {
					sb.Append(expression);
				} else {
					string name = _getVariableName.Get(column.Name);
					sb.Append(name);
					options.Variables[name] = typeBuilder.Build(column);
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
