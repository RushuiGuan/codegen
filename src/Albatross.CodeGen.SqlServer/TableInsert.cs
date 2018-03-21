using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_insert", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Insert statement that excludes the computed columns")]
	public class TableInsert : TableQueryGenerator {
		IGetTable getTable;
		ICreateVariableName getVariableName;
		IBuildSqlType buildSqlType;
		ICreateVariable createVariable;

		public TableInsert(IGetTable getTable, ICreateVariableName getVariableName, IBuildSqlType buildSqlType, ICreateVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.buildSqlType = buildSqlType;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table table, SqlCodeGenOption options) {
			foreach (var item in options.Variables) {
				createVariable.Create(this, item);
			}

			Column[] columns = (from c in table.Columns where !c.IsIdentity && !c.IsComputed select c).ToArray();
			if (columns.Length == 0) {
				throw new CodeGenException("Editable column not found");
			}
			sb.Append($"insert into [{table.Schema}].[{table.Name}] ").OpenParenthesis();
			foreach (Column c in columns) {
				sb.EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma().Space(); }
			}
			sb.CloseParenthesis().AppendLine().Append("values ").OpenParenthesis();
			foreach (Column c in columns) {
				if (options.Expressions.TryGetValue(c.Name, out string expression)) {
					sb.Append(expression);
				} else {
					string name = getVariableName.Get(c.Name);
					sb.Append(name);
					createVariable.Create(this, c.GetVariable()); 
				}
				if (c != columns.Last()) { sb.Comma().Space(); }
			}
			sb.CloseParenthesis();
			return new[] { this };
		}
	}
}
