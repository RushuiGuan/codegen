using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Constructor {
		public AccessModifier AccessModifier { get; set; }
		public IEnumerable<Parameter> Parameters { get; set; }
		public bool Static { get; set; }
		public Constructor ChainedConstructor { get; set; }
		public StringBuilder Body { get; private set; } = new StringBuilder();
	}
}
