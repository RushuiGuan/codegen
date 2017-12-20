using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Step {
		public Type SourceType { get; set; }
		public object Source { get; set; }
		public string Generator { get; set; }
	}

	public class StepDefinition {
		public string SourceType { get; set; }
		public string Source { get; set; }
		public string Generator { get; set; }
	}
}
