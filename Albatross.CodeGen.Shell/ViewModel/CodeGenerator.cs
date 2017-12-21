using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CodeGenerator { 
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string Target { get; set; }
		public string Type { get; set; }
		public string Assembly { get; set; }
		public string Location { get; set; }
	}
}
