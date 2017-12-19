using Albatross.CodeGen.Database;
using Dapper;

using System.Data.SqlClient;

namespace Albatross.CodeGen.SqlServer {
	public class GetTableIdentityColumn : IGetTableIdentityColumn {
		public GetTableIdentityColumn() {
		}

		public Column Get(Table table) {
			string sql = this.GetType().GetAssemblyResource("Albatross.CodeGen.SqlServer.GetTableIdentityColumn.sql");
			using (var db = new SqlConnection(table.GetConnectionString())) {
				return db.QueryFirstOrDefault<Column>(sql, new { schema = table.Schema, table = table.Name }, commandType: System.Data.CommandType.Text);
			}
		}
	}
}
