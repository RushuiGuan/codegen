using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
	public class Leaf : INode {
		public Leaf(string name) {
			Name = name;
		}
		public string Name { get; private set; }

		public object Source { get; set; }
	}
}
