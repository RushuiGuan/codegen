using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class GetDbConnection : IGetDbConnection {
		IGetConnectionString getConnectionString;

		public GetDbConnection(IGetConnectionString getConnectionString) {
			this.getConnectionString = getConnectionString;
		}

		public IDbConnection Get(Server server) {
			string connectionString = getConnectionString.Get(server);
			return new SqlConnection(connectionString);
		}
	}
}
