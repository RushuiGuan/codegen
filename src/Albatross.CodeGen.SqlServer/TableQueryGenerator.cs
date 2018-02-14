using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public abstract class TableQueryGenerator : ICodeGenerator<DatabaseObject, SqlCodeGenOption> {

		public string Category => "Sql Server";
		public string Target => "sql";

		public Type SourceType => typeof(DatabaseObject);
		public Type OptionType => typeof(SqlCodeGenOption);

		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public abstract IEnumerable<object> Build(StringBuilder sb, DatabaseObject source, SqlCodeGenOption option);

		public void Configure(object data) { }
	}
}
