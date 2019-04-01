using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Model {
	public class Method {
		public Method() { }
		public Method(string name) {
			Name = name;
		}

		public DotNetType ReturnType { get; set; } = DotNetType.Void;
		public string Name { get; set; }
		public AccessModifier AccessModifier { get; set; }
		public IEnumerable<Parameter> Parameters { get; set; }
		public bool Static { get; set; }
		public bool Virtual { get; set; }
		public bool Override { get; set; }
		public CodeBlock Body { get; set; } 
	}
}
