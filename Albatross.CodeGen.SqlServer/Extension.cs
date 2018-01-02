using Albatross.CodeGen.Database;
using System.Data.SqlClient;

namespace Albatross.CodeGen.SqlServer {
	public static class Extension
    {
		public static string GetConnectionString(this Table table) {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
				DataSource = table.Server.DataSource,
				InitialCatalog = table.Server.InitialCatalog,
				IntegratedSecurity = true
			};
			return builder.ToString();
		}

		public static Composite NewSqlTableComposite(string name, string description, params string[] children) {
			return new Composite {
				Name = name,
				Description = description,
				Category = "Sql Server",
				Generators = children,
				SourceType = typeof(Table),
				Target = "sql",
				Seperator = "\n",
			};
		}
	}
}
