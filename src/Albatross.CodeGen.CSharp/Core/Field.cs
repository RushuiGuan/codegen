using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Field {
		public string Name { get; set; }
		public string Type { get; set; }
		public AccessModifier Modifier { get; set; }
		public bool ReadOnly { get; set; } 
		public bool Static { get; set; }
	}
}
