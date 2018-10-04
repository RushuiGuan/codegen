using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Parameter {
		public string Name { get; set; }
		public DotNetType Type { get; set; }
		public ParameterModifier Modifier {get;set;}
	}
}
