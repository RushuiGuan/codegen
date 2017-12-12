using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class DbConnectionSetup : IGetDbConnectionString, ISetDbConnection {
		DbConnection _conn;
		public string Get() {
			if (_conn != null) {
				SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
				builder.DataSource = _conn.Server;
				builder.InitialCatalog = _conn.Database;
				builder.IntegratedSecurity = _conn.IntegratedSecurity;
				if (!builder.IntegratedSecurity) {
					builder.UserID = _conn.UserID;
					builder.Password = _conn.Password;
				}
				return builder.ToString();
			} else {
				return string.Empty;
			}
		}

		public void Set(DbConnection conn) {
			_conn = conn;
		}
	}
}
