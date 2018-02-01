
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
		IGetTableColumns getTableColumns;
		IGetVariableName getVariableName;
		IGetTablePrimaryKey getPrimary;
		IColumnSqlTypeBuilder typeBuilder;
		ICreateVariable createVariable;

		public TableUpdate(IGetTableColumns getTableColumns, IGetTablePrimaryKey getPrimary, IGetVariableName getVariableName, IColumnSqlTypeBuilder typeBuilder, ICreateVariable createVariable) {
			this.getTableColumns = getTableColumns;
			this.getVariableName = getVariableName;
			this.getPrimary = getPrimary;
			this.typeBuilder = typeBuilder;
			this.createVariable = createVariable;
		}

		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption option, ICodeGeneratorFactory factory) {
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
				createVariable.Create(this, item.Key, item.Value);
			}
			foreach (Column c in columns) {
				name = getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ");
				if (option.Expressions.TryGetValue(c.Name, out string expression)) {
					sb.Append(expression);
				} else {
					createVariable.Create(this, name,typeBuilder.Build(c));
					sb.Append(name);
				}
				if (c != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return sb;
		}

	}
}
