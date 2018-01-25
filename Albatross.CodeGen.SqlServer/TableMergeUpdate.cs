﻿using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeUpdate : TableQueryGenerator {
		IGetTableColumns getColumns;
		IGetVariableName getVariableName;
		IGetTablePrimaryKey getPrimary;
		IColumnSqlTypeBuilder typeBuilder;

		public TableMergeUpdate(IGetTableColumns getColumns, IGetVariableName getVariableName, IColumnSqlTypeBuilder typeBuilder, IGetTablePrimaryKey getPrimary) {
			this.getColumns = getColumns;
			this.getVariableName = getVariableName;
			this.typeBuilder = typeBuilder;
			this.getPrimary = getPrimary;
		}

		public override string Name => "table_merge_update";
		public override string Description => "Table merge update clause";

		public override StringBuilder Build(StringBuilder sb, Table table, SqlQueryOption options, ICodeGeneratorFactory factory) {
			HashSet<string> keys = new HashSet<string>();
			if (options.ExcludePrimaryKey) {
				keys.AddRange(from item in getPrimary.Get(table) select item.Name);
			}

			Column[] columns = (from c in getColumns.Get(table)
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.ComputedColumn && !c.IdentityColumn && !keys.Contains(c.Name)
								select c).ToArray();
			if (columns.Length == 0) {
				//merge without update is OK
				return sb;
			}
			sb.Append("when matched then update set").AppendLine();
			foreach (var column in columns) {
				sb.Tab().EscapeName(column.Name).Append(" = src.").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return sb;
		}
	}
}
