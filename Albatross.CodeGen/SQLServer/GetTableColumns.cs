using System.IO;
using Dapper;
using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Albatross.DbScripting.SqlServer {
	public class GetTableColumns : IGetTableColumns {
		IGetDbConnectionString _getDbConnString;

		public GetTableColumns(IGetDbConnectionString getDbConnString) {
			_getDbConnString = getDbConnString;
		}

		string GetQuery() {
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Albatross.Db.SqlServer.GetTableColumns.sql")) {
				return new StreamReader(stream).ReadToEnd();
			}
		}

		public IEnumerable<Column> Get(string schema, string table) {
			string sql = this.GetType().GetTypeEmbeddedResource(".sql");
			using (var db = new SqlConnection(_getDbConnString.Get())) {
				return db.Query<Column>(sql, new { schema = schema, table = table }, commandType: System.Data.CommandType.Text);
			}
		}
	}
}
