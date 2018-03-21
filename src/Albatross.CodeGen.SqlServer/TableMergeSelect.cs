using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_merge_select", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement select clause")]
	public class TableMergeSelect : TableQueryGenerator {
		IGetTable getTable;
		ICreateVariableName getVariableName;
		IBuildSqlType buildSqlType;
		ICreateVariable createVariable;

		public TableMergeSelect(IGetTable getTable, ICreateVariableName getVariableName, IBuildSqlType buildSqlType, ICreateVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.buildSqlType = buildSqlType;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object>  Build(StringBuilder sb, Table table, SqlCodeGenOption options) {
			table = getTable.Get(table.Database, table.Schema, table.Name);
			if (table.Columns.Count() == 0) {
				throw new CodeGenException("Editor column doesn't exist");
			}
			sb.Append("using ").OpenParenthesis().AppendLine();
			sb.Tab().AppendLine("select");
			foreach (var column in table.Columns) {
				sb.Tab(2);
				if (options.Expressions.TryGetValue(column.Name, out string expression)) {
					sb.Append(expression);
				} else {
					string name = getVariableName.Get(column.Name);
					sb.Append(name);
					createVariable.Create(this, column.GetVariable());
				}
				sb.Append(" as ").EscapeName(column.Name);
				if (column != table.Columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" as src");
			return new[] { this, };
		}
	}
}
