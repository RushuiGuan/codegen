
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableWhere : TableQueryGenerator {
		IGetTableIdentityColumn getIDColumn;
		IGetVariableName getVariableName;
		IGetTablePrimaryKey getPrimary;
		IColumnSqlTypeBuilder typeBuilder;

		public override string Name => "table_where";
		public override string Description => "Table where clause, can handle identity column and\\or primary keys.  Use the SqlQueryOption.Filter flag to indicate filter method";

		public TableWhere(IGetTableIdentityColumn getIDColumn, IGetVariableName getVariableName, IGetTablePrimaryKey getPrimary, IColumnSqlTypeBuilder typeBuilder) {
			this.getIDColumn = getIDColumn;
			this.getVariableName = getVariableName;
			this.getPrimary = getPrimary;
			this.typeBuilder = typeBuilder;
		}

		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption option, ICodeGeneratorFactory factory) {
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
			return sb;
		}

		void AppendColumn(StringBuilder sb, Column c, int count, SqlQueryOption option) {
			sb.AppendLine().Tab();
			if (count > 0) { sb.Append("and "); }
			string variable = getVariableName.Get(c.Name);
			sb.EscapeName(c.Name).Append(" = ").Append(variable);
			option.Variables[variable] = typeBuilder.Build(c);
		}
	}
}
