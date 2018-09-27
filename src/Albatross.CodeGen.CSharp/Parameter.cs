using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class Parameter {
		public string Name { get; set; }
		public string Type { get; set; }
		public ParameterModifier Modifier {get;set;}
	}
}
