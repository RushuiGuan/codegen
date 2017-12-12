using Dapper;
using Albatross.DbScripting.Core;
using System.Data.SqlClient;
using System.Linq;

namespace Albatross.DbScripting.SqlServer {
	public class GetTableIdentityColumn : IGetTableIdentityColumn {
		IGetDbConnectionString _getConnString;
		public GetTableIdentityColumn(IGetDbConnectionString getConnString) {
			_getConnString = getConnString;
		}

		public Column Get(string schema, string table) {
			string sql = this.GetType().GetTypeEmbeddedResource(".sql");
			using (var db = new SqlConnection(_getConnString.Get())) {
				return db.QueryFirstOrDefault<Column>(sql, new { schema = schema, table = table }, commandType: System.Data.CommandType.Text);
			}
		}
	}
}
