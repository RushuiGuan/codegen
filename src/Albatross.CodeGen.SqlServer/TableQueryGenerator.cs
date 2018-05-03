using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public abstract class TableQueryGenerator : ICodeGenerator<Table, SqlCodeGenOption> {

		public string Category => "Sql Server";
		public string Target => "sql";

		public Type SourceType => typeof(Table);
		public Type OptionType => typeof(SqlCodeGenOption);

		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public abstract IEnumerable<object> Build(StringBuilder sb, Table table, SqlCodeGenOption option);

		public IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			return this.ValidateNBuild(sb, source, option);
		}

		public void Configure(object data) { }
	}
}
