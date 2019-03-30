using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Model {
	public class Property {
		public Property(string name) {
			this.Name = name;
		}
		public Property() { }

		public string Name { get; set; }
		public DotNetType Type { get; set; }
		public AccessModifier Modifier { get; set; } = AccessModifier.Public;
		public AccessModifier SetModifier { get; set; } = AccessModifier.Public;
		public bool Static { get; set; }
        public bool CanWrite { get; set; }
        public bool CanRead { get; set; }
    }
}
