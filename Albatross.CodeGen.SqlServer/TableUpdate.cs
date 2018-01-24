
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableUpdate : TableQueryGenerator {
		IGetTableColumns getTableColumns;
		IGetVariableName getVariableName;
		IGetTablePrimaryKey getPrimary;

		public override string Name => "table_update";
		public override string Description => "Update statement that excludes the computed columns";

		public TableUpdate(IGetTableColumns getTableColumns, IGetTablePrimaryKey getPrimary, IGetVariableName getVariableName) {
			this.getTableColumns = getTableColumns;
			this.getVariableName = getVariableName;
			this.getPrimary = getPrimary;
		}

		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			SqlQueryOption option = options as SqlQueryOption;
			if (option == null) {
				option = new SqlQueryOption();
			}
			HashSet<string> keys = new HashSet<string>();
			if (option.ExcludePrimaryKey) {
				keys.AddRange(from item in getPrimary.Get(t) select item.Name);
			}
			getPrimary.Get(t);

			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			Column[] columns = (from c in getTableColumns.Get(t).ToArray()
								where !c.IdentityColumn && !c.ComputedColumn && !keys.Contains(c.Name)
								select c).ToArray();

			string name;
			foreach (Column c in columns) {
				name = getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ").Append(name);
				if (c != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return sb;
		}

	}
}
