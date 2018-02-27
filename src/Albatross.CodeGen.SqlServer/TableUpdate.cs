
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_update", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Update statement that excludes the computed columns.  Will exclude the primary key by default unless ExcludePrimaryKey flag is set to false in the option")]
	public class TableUpdate : TableQueryGenerator {
		IGetTableColumn getTableColumns;
		IGetVariableName getVariableName;
		IGetTablePrimaryKey getPrimary;
		ICreateVariable createVariable;

		public TableUpdate(IGetTableColumn getTableColumns, IGetTablePrimaryKey getPrimary, IGetVariableName getVariableName, ICreateVariable createVariable) {
			this.getTableColumns = getTableColumns;
			this.getVariableName = getVariableName;
			this.getPrimary = getPrimary;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object>  Build(StringBuilder sb, DatabaseObject t, SqlCodeGenOption option) {
			HashSet<string> keys = new HashSet<string>();
			if (option.ExcludePrimaryKey) {
				keys.AddRange(from item in getPrimary.Get(t) select item.Name);
			}

			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			Column[] columns = (from c in getTableColumns.Get(t).ToArray()
								where !c.IdentityColumn && !c.ComputedColumn && !keys.Contains(c.Name)
								select c).ToArray();

			string name;
			foreach (var item in option.Variables) {
				createVariable.Create(this, item);
			}
			foreach (Column c in columns) {
				name = getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ");
				if (option.Expressions.TryGetValue(c.Name, out string expression)) {
					sb.Append(expression);
				} else {
					createVariable.Create(this, c.GetVariable());
					sb.Append(name);
				}
				if (c != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return new[] { this, };
		}
	}
}
