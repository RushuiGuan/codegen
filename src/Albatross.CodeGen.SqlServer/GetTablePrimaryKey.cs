using Dapper;

using System.Data.SqlClient;
using System.Collections.Generic;
using Albatross.CodeGen.Database;

namespace Albatross.CodeGen.SqlServer {
	public class GetTablePrimaryKey : IGetTablePrimaryKey {


		public IEnumerable<Column> Get(DatabaseObject table) {
			string sql = this.GetType().GetAssemblyResource("Albatross.CodeGen.SqlServer.GetTablePrimaryKey.sql");
			using (var db = new SqlConnection(table.GetConnectionString())) {
				return db.Query<Column>(sql, new { schema = table.Schema, table = table.Name }, commandType: System.Data.CommandType.Text);
			}
		}
	}
}
