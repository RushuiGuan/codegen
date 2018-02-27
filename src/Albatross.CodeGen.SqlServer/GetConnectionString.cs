using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class GetConnectionString : IGetConnectionString {
		public string Get(Server server) {
			if (string.IsNullOrEmpty(server.ConnectionString)) {
				if (server.SSPI) {
					return $"Server=${server.DataSource}; Database=${server.InitialCatalog}; Trusted_Connection=True";
				} else {
					return $"Server=${server.DataSource}; Database=${server.InitialCatalog}; User Id=${server.UserName}; Password=${server.Password}";
				}
			} else {
				return server.ConnectionString;
			}
		}
	}
}
