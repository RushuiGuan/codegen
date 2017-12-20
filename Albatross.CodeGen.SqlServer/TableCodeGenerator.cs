using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public abstract class TableCodeGenerator : ICodeGenerator<Table> {
		public abstract string Name { get; }
		public abstract string Description { get; }

		public string Category => "Sql Server";
		public string Target => "sql";

		public Type SourceType => typeof(Table);

		public abstract StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory);

		public StringBuilder Build(StringBuilder sb, object t, object options, ICodeGeneratorFactory factory) {
			return Build(sb, (Table)t, options, factory);
		}
	}
}
