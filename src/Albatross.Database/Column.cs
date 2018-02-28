using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.Database {
	public class Column {
		public string Name { get; set; }
		public SqlType Type { get; set; }

		public bool IsNullable { get; set; }
		public bool IsIdentity { get; set; }
		public bool IsComputed { get; set; }
		public bool IsFilestream { get; set; }
	}
}
