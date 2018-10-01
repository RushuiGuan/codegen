using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Method {
		public string ReturnType { get; set; }
		public string Name { get; set; }
		public AccessModifier AccessModifier { get; set; }
		public IEnumerable<Parameter> Parameters { get; set; }
		public bool Static { get; set; }
		public bool Virtual { get; set; }
		public bool Override { get; set; }
		public StringBuilder Body { get; private set; } = new StringBuilder();
	}
}
