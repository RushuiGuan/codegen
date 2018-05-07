using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Faults;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.insert", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Insert statement that excludes the computed columns")]
	public class InsertStatement : TableQueryGenerator {
		IGetTable getTable;
		ICreateSqlVariableName getVariableName;
		IRenderSqlType buildSqlType;
		IStoreSqlVariable createVariable;

		public InsertStatement(IGetTable getTable, ICreateSqlVariableName getVariableName, IRenderSqlType buildSqlType, IStoreSqlVariable createVariable) {
			this.getTable = getTable;
			this.getVariableName = getVariableName;
			this.buildSqlType = buildSqlType;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object> Generate(StringBuilder sb, Table table, SqlCodeGenOption options) {
			Column[] columns = (from c in table.Columns?? new Column[0] where !c.IsIdentity && !c.IsComputed select c).ToArray();
			if (columns.Length == 0) {
				throw new CodeGeneratorException("Editable column not found");
			}
			sb.Append($"insert into [{table.Schema}].[{table.Name}] ").OpenParenthesis();
			foreach (Column c in columns) {
				sb.EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma().Space(); }
			}
			sb.CloseParenthesis().AppendLine().Append("values ").OpenParenthesis();
			foreach (Column c in columns) {
				string name = getVariableName.Get(c.Name);
				sb.Append(name);
				createVariable.Store(this, c.GetVariable()); 
				if (c != columns.Last()) { sb.Comma().Space(); }
			}
			sb.CloseParenthesis();
			return new[] { this };
		}
	}
}
