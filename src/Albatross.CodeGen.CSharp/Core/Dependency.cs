using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Dependency {
		public Dependency(string name) {
			Name = name;
		}
		public Dependency() { }

		public DotNetType Type { get; set; }
		public DotNetType FieldType { get; set; }
		public string Name { get; set; }
	}
}
