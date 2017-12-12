using Dapper;
using Albatross.DbScripting.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace Albatross.DbScripting.SqlServer {
	public class GetTablePrimaryKey : IGetTablePrimaryKey {
		IGetDbConnectionString _getConnString;
		public GetTablePrimaryKey(IGetDbConnectionString getConnString) {
			_getConnString = getConnString;
		}

		public IEnumerable<Column> Get(string schema, string table) {
			string sql = this.GetType().GetTypeEmbeddedResource(".sql");
			using (var db = new SqlConnection(_getConnString.Get())) {
				return db.Query<Column>(sql, new { schema = schema, table = table }, commandType: System.Data.CommandType.Text);
			}
		}
	}
}
