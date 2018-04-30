using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
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
		IStoreVariable createVariable;

		public TableInsert(IGetTable getTable, ICreateVariableName getVariableName, IBuildSqlType buildSqlType, IStoreVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.buildSqlType = buildSqlType;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table table, SqlCodeGenOption options) {
			table = getTable.Get(table.Database, table.Schema, table.Name);
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
				string name = getVariableName.Get(c.Name);
				if (options.Expressions.TryGetValue(name, out string expression)) {
					sb.Append(expression);
				} else {
					sb.Append(name);
					createVariable.Store(this, c.GetVariable()); 
				}
				if (c != columns.Last()) { sb.Comma().Space(); }
			}
			sb.CloseParenthesis();
			return new[] { this };
		}
	}
}
