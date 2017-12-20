using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Composite {
		public Type SourceType { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public IEnumerable<string> Generators { get; set; }
		public string Target { get; set; }
		public string Seperator { get; set; }
	}
}
