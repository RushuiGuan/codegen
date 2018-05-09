using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Faults;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.update", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Update statement that excludes the computed columns.  Will exclude the primary key by default unless ExcludePrimaryKey flag is set to false in the option")]
	public class UpdateStatement : TableQueryGenerator {
		IGetTable getTable;
		ICreateSqlVariableName getVariableName;
		IStoreSqlVariable createVariable;

		public UpdateStatement(IGetTable getTable, ICreateSqlVariableName getVariableName, IStoreSqlVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object>  Generate(StringBuilder sb, Table t, SqlCodeGenOption option) {
			HashSet<string> keys = new HashSet<string>();
			keys.AddRange(from item in t.PrimaryKeys select item.Name);

			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			Column[] columns = (from c in t.Columns ?? new Column[0]
								where !c.IsIdentity && !c.IsComputed && !keys.Contains(c.Name)
								select c).ToArray();

			if (columns.Length == 0) {
				throw new CodeGeneratorException("No editable column found");
			}
			string name;
			foreach (Column c in columns) {
				name = getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ");
				createVariable.Store(this, c.GetVariable());
				sb.Append(name);
				if (c != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return new[] { this, };
		}
	}
}
