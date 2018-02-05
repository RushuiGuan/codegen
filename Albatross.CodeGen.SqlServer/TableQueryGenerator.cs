using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public abstract class TableQueryGenerator : ICodeGenerator<DatabaseObject, SqlQueryOption> {

		public string Category => "Sql Server";
		public string Target => "sql";

		public Type SourceType => typeof(DatabaseObject);
		public Type OptionType => typeof(SqlQueryOption);

		public event Func<StringBuilder, ICodeGeneratorFactory, IEnumerable<object>> Yield;
		public abstract StringBuilder Build(StringBuilder sb, DatabaseObject source, SqlQueryOption option, ICodeGeneratorFactory factory, out IEnumerable<object> used);
	}
}
