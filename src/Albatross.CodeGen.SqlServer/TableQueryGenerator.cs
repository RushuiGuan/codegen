using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public abstract class TableQueryGenerator : ICodeGenerator<Table, SqlCodeGenOption> {
		public int TabLevel { get; set; }

		public string Category => "Sql Server";
		public string Target => "sql";

		public Type SourceType => typeof(Table);
		public Type OptionType => typeof(SqlCodeGenOption);

		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }
		public abstract IEnumerable<object> Generate(StringBuilder sb, Table table, SqlCodeGenOption option);

		public IEnumerable<object> Generate(StringBuilder sb, object source, ICodeGeneratorOption option) {
			return this.ValidateNGenerate(sb, source, option);
		}

		public void Configure(object data) { }
	}
}
