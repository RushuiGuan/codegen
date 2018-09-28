using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class DotNetType {
		public string Name { get; set; }
		public bool IsGeneric { get; set; }
		public IEnumerable<DotNetType> GenericTypes { get; set; }
	}
}
