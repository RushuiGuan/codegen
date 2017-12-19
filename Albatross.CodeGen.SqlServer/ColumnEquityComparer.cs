using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;

namespace Albatross.CodeGen.SqlServer {
	public class ColumnEquityComparer : IEqualityComparer<Column> {
		public bool Equals(Column x, Column y) {
			return string.Equals(x?.Name, y?.Name, StringComparison.InvariantCultureIgnoreCase);
		}

		public int GetHashCode(Column obj) {
			if (obj == null) {
				return 0;
			} else {
				return obj.Name.GetHashCode();
			}
		}
	}
}
