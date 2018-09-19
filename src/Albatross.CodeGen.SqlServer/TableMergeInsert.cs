using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table-merge-insert", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement insert clause")]
	public class TableMergeInsert : TableQueryGenerator {
		IGetTable getTable;
		ICreateVariableName createVariableName;

		public TableMergeInsert(IGetTable getTable, ICreateVariableName createVariableName) {
			this.getTable = getTable;
			this.createVariableName = createVariableName;
		}


		public override IEnumerable<object>  Build(StringBuilder sb, Table t, SqlCodeGenOption options) {
			t = getTable.Get(t.Database, t.Schema, t.Name);

			Column[] columns = (from c in t.Columns
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.IsComputed && !c.IsIdentity
								select c).ToArray();
			if (columns.Length == 0) {
				throw new CodeGenException("Editable column not found");
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
