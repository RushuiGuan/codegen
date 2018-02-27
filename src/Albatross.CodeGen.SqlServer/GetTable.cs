using System.Linq;
using Dapper;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public class GetTable : IGetTable {
		IGetDbConnection getDbConnection;
		IGetSchema getSchema;

		public GetTable(IGetDbConnection getDbConnection, IGetSchema getSchema) {
			this.getDbConnection = getDbConnection;
			this.getSchema = getSchema;
		}


		public Table Get(Server server, string schema, string name) {
			var schemaObject = getSchema.Get(server, schema);
			using (var db = getDbConnection.Get(server)) {
				return db.QueryFirst<Table>("select * from sys.tables where name = @name and schema_id = @schema", new { name = name, schema = schemaObject.Schema_Id });
			}
		}
	}
}
