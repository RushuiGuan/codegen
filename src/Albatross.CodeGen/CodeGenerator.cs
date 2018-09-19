using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CodeGenerator{
		public string Name { get; set; }
		public string Target { get; set; }

		public string Category { get; set; }
		public string Description { get; set; }

		public Type SourceType { get; set; }
		public Type OptionType { get; set; }
		public Type GeneratorType { get; set; }
		public object Data { get; set; }
	}
}
