using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	[AttributeUsage(AttributeTargets.Class)]
	public class CodeGeneratorAttribute : Attribute {

		public CodeGeneratorAttribute(string name, string target) {
			Name = name;
			Target = target;
		}
		public string Name { get; private set; }
		public string Target { get; private set; }

		public string Category { get; set; }
		public string Description { get; set; }
	}
}
