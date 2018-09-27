using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class MethodExpression {
		public string Name { get; set; }
		public IEnumerable<Parameter> Parameters { get; set; }
	}
}
