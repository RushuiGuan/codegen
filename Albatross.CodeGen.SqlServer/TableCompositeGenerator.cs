using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class TableCompositeGenerator : CompositeCodeGenrator<Table> {
		public TableCompositeGenerator(string name, string description, params string[] generators) : base(name, "Sql Server", description, "sql", generators) {
			Seperator = "\r\n";
		}
	}
}
