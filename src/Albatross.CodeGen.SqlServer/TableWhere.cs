
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_where", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Table where clause, can handle identity column and\\or primary keys.  Use the SqlQueryOption.Filter flag to indicate filter method")]
	public class TableWhere : TableQueryGenerator {
		IGetTableIdentityColumn getIDColumn;
		IGetVariableName getVariableName;
		IGetTablePrimaryKey getPrimary;
		IColumnSqlTypeBuilder typeBuilder;
		ICreateVariable createVariable;

		public TableWhere(IGetTableIdentityColumn getIDColumn, IGetVariableName getVariableName, IGetTablePrimaryKey getPrimary, IColumnSqlTypeBuilder typeBuilder, ICreateVariable createVariable) {
			this.getIDColumn = getIDColumn;
			this.getVariableName = getVariableName;
			this.getPrimary = getPrimary;
			this.typeBuilder = typeBuilder;
			this.createVariable = createVariable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, DatabaseObject t, SqlCodeGenOption option) {
			sb.Append("where");
			int count = 0;

			if ((option.Filter & FilterOption.ByIdentityColumn) > 0){
				Column column = getIDColumn.Get(t);
				if (column == null) {
					throw new IdentityColumnNotFoundException(t);
				}
				AppendColumn(sb, column, count, option);
				count++;
			}

			if ((option.Filter & FilterOption.ByPrimaryKey) > 0) {
				var columns = getPrimary.Get(t);
				foreach (var column in columns) {
					AppendColumn(sb, column, count, option);
					count++;
				}
			}
			return new[] { this };
		}

		void AppendColumn(StringBuilder sb, Column c, int count, SqlCodeGenOption option) {
			sb.AppendLine().Tab();
			if (count > 0) { sb.Append("and "); }
			string variable = getVariableName.Get(c.Name);
			sb.EscapeName(c.Name).Append(" = ").Append(variable);
			createVariable.Create(this, c.GetVariable());
		}
	}
}
