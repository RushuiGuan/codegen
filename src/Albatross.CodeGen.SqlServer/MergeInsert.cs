using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Faults;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.merge.insert", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement insert clause")]
	public class MergeInsert : TableQueryGenerator {
		IGetTable getTable;
		ICreateSqlVariableName createVariableName;

		public MergeInsert(IGetTable getTable, ICreateSqlVariableName createVariableName) {
			this.getTable = getTable;
			this.createVariableName = createVariableName;
		}


		public override IEnumerable<object>  Generate(StringBuilder sb, IDictionary<string, string> customCode, Table t, SqlCodeGenOption options) {
			Column[] columns = (from c in t.Columns??new Column[0]
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.IsComputed && !c.IsIdentity
								select c).ToArray();
			if (columns.Length == 0) {
				throw new CodeGeneratorException("Editable column not found");
			}
			sb.Append("when not matched by target then insert ").OpenParenthesis().AppendLine();
			foreach (var column in columns) {
				sb.Tab().EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" values ").OpenParenthesis().AppendLine();
			foreach (var column in columns) {
				sb.Tab().Append("src.").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis();
			return new[] { this };
		}
	}
}
