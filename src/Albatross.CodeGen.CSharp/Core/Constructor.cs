using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Constructor : Method {
		public Constructor(string name) : base(name) { }
		public Constructor() { }

		public Constructor ChainedConstructor { get; set; }
	}
}
