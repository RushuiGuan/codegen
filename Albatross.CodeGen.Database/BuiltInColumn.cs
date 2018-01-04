using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	public abstract class BuiltInColumn {
		public BuiltInColumn(string name) {
			Name = name;
		}
		public string Name { get; private set; }

		public abstract bool Match(Column c);
	}
}
