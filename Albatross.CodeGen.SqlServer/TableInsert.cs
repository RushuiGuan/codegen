using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableInsert : TableQueryGenerator {
		IGetTableColumns getTableColumns;
		IGetVariableName getVariableName;
		IColumnSqlTypeBuilder sqlTypeBuilder;
		ICreateVariable createVariable;

		public TableInsert(IGetTableColumns getTableColumns, IGetVariableName getVariableName, IColumnSqlTypeBuilder sqlTypeBuilder, ICreateVariable createVariable) {
			this.getTableColumns = getTableColumns;
			this.getVariableName = getVariableName;
			this.sqlTypeBuilder = sqlTypeBuilder;
			this.createVariable = createVariable;
		}

		public override string Name => "table_insert";
		public override string Description => "Insert statement that excludes the computed columns";

		public override StringBuilder Build(StringBuilder sb, DatabaseObject table, SqlQueryOption options, ICodeGeneratorFactory factory) {
			foreach (var item in options.Variables) {
				createVariable.Create(this, item.Key, item.Value);
			}

			Column[] columns = (from c in getTableColumns.Get(table) where !c.IdentityColumn && !c.ComputedColumn select c).ToArray();
			if (columns.Length == 0) {
				throw new ColumnNotFoundException(table.Schema, table.Name);
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
					createVariable.Create(this, name, sqlTypeBuilder.Build(c));
				}
				if (c != columns.Last()) { sb.Comma().Space(); }
			}
			sb.CloseParenthesis();
			return sb;
		}
	}
}
