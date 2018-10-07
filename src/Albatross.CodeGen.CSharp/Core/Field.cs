using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Field {
		public Field(string name) {
			Name = name;
		}
		public Field() { }
		public string Name { get; set; }
		public DotNetType Type { get; set; }
		public AccessModifier Modifier { get; set; }
		public bool ReadOnly { get; set; } 
		public bool Static { get; set; }

		public StringBuilder Assignment { get; private set; } = new StringBuilder();
	}
}
