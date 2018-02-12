using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Albatross.CodeGen.Database;
using Dapper;

namespace Albatross.CodeGen.SqlServer {
	public class GetTableColumns : IGetTableColumns {
		public IEnumerable<Column> Get(DatabaseObject table) {
			using (var db = new SqlConnection(table.GetConnectionString())) {
				string sql = typeof(GetTableColumns).GetAssemblyResource("Albatross.CodeGen.SqlServer.GetTableColumns.sql");
				return db.Query<Column>(sql, new { schema = table.Schema, table = table.Name });
			}
		}
	}
}
