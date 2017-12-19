using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class BuiltInDateTimeColumn : BuiltInColumn {
		public BuiltInDateTimeColumn(string name) : base(name) {
		}

		public override bool Match(Column c) {
			return string.Equals(c.Name, Name, StringComparison.InvariantCultureIgnoreCase) && c.IsDateTime();
		}
	}
}
