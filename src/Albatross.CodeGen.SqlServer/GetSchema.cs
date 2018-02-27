using System.Linq;
using Dapper;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public class GetSchema : IGetSchema {
		IGetDbConnection getDbConnection;

		public GetSchema(IGetDbConnection getDbConnection) {
			this.getDbConnection = getDbConnection;
		}

		public Schema Get(Server server, string name) {
			using (var db = getDbConnection.Get(server)) {
				return db.QueryFirst<Schema>("select * from sys.schemas where name = @name", new { name = name, });
			}
		}
	}
}
