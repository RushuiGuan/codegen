using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Model {
	public class Variable {
		public Variable(string name) {
			Name = name;
		}
		public Variable() { }
		public string Name { get; set; }
		public DotNetType Type { get; set; }
		public ParameterModifier Modifier {get;set;}
	}
}
