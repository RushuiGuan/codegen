using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Scenario {
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }

		public Step[] Steps { get; set; }
	}
}
