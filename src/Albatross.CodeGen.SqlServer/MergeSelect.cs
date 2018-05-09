using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Faults;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.merge.select", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement select clause")]
	public class MergeSelect : TableQueryGenerator {
		IGetTable getTable;
		ICreateSqlVariableName getVariableName;
		IRenderSqlType buildSqlType;
		IStoreSqlVariable createVariable;

		public MergeSelect(IGetTable getTable, ICreateSqlVariableName getVariableName, IRenderSqlType buildSqlType, IStoreSqlVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.buildSqlType = buildSqlType;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object>  Generate(StringBuilder sb, Table table, SqlCodeGenOption options) {
			if (!(table.Columns?.Count() > 0)) {
				throw new CodeGeneratorException("Editor column doesn't exist");
			}
			sb.Append("using ").OpenParenthesis().AppendLine();
			sb.Tab().AppendLine("select");
			foreach (var column in table.Columns) {
				sb.Tab(2);
				string name = getVariableName.Get(column.Name);
				sb.Append(name);
				createVariable.Store(this, column.GetVariable());
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
