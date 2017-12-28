using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	[SourceType("Sql server table or view column")]
	public class Column {
		public string Name { get; set; }
		public int OrdinalPosition { get; set; }
		public bool IsNullable { get; set; }
		public string DataType { get; set; }
		public int MaxLength { get; set; }
		public int Precision { get; set; }
		public int Scale { get; set; }
		public bool IdentityColumn { get; set; }
		public bool ComputedColumn { get; set; }
	}
}
