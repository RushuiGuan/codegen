
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_update", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Update statement that excludes the computed columns.  Will exclude the primary key by default unless ExcludePrimaryKey flag is set to false in the option")]
	public class TableUpdate : TableQueryGenerator {
		IGetTable getTable;
		ICreateVariableName getVariableName;
		IStoreVariable createVariable;

		public TableUpdate(IGetTable getTable, ICreateVariableName getVariableName, IStoreVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object>  Build(StringBuilder sb, Table t, SqlCodeGenOption option) {
			t = getTable.Get(t.Database, t.Schema, t.Name);

			HashSet<string> keys = new HashSet<string>();
			if (option.ExcludePrimaryKey) {
				keys.AddRange(from item in t.PrimaryKeys select item.Name);
			}

			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			Column[] columns = (from c in t.Columns.ToArray()
								where !c.IsIdentity && !c.IsComputed && !keys.Contains(c.Name)
								select c).ToArray();

			string name;
			foreach (Column c in columns) {
				name = getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ");

				if (option.Expressions.TryGetValue(name, out string expression)) {
					sb.Append(expression);
				} else {
					createVariable.Store(this, c.GetVariable());
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
