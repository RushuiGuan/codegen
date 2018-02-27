using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Albatross.CodeGen.Database;
using Dapper;

namespace Albatross.CodeGen.SqlServer {
	public class GetTablePrimaryKey : IGetTablePrimaryKey {
		IGetDbConnection getDbConnection;
		IGetTable getTable;

		public GetTablePrimaryKey(IGetDbConnection getDbConnection, IGetTable getTable) {
			this.getDbConnection = getDbConnection;
			this.getTable = getTable;
		}

		public IEnumerable<Column> Get(Server server, string schema, string name) {
			Table table = getTable.Get(server, schema, name);
			using (var db = getDbConnection.Get(server)) {
				return db.Query<Column>(query, new { object_id = table.Object_Id});
			}
		}

		string @query = @"select c.* 
from sys.indexes i 
join sys.index_columns ic on i.object_id = ic.object_id and i.index_id = ic.index_id
join sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id
where i.object_id = @object_id and i.is_primary_key = 1
order by ic.key_ordinal;";
	}
}
