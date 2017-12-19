
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class BuiltInStringColumn : BuiltInColumn {
		public BuiltInStringColumn(string name) : base(name) {
		}

		public override bool Match(Column c) {
			return string.Equals(c.Name, Name, StringComparison.InvariantCultureIgnoreCase) && c.IsString();
		}
	}
}
