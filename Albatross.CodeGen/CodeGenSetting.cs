using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CodeGenSetting {
		public IEnumerable<string> AssemblyLocations { get; set; }
		public IEnumerable<string> CompositeLocations { get; set; }
		public IEnumerable<string> ScenarioLocations { get; set; }
	}
}
