using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class Constructor {
		public AccessModifier AccessModifier { get; set; }
		public IEnumerable<Parameter> Parameters { get; set; }
		public bool Static { get; set; }
		public MethodExpression Chain { get; set; }
	}
}
