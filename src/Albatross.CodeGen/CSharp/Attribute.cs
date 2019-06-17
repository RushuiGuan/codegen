using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class Attribute {
		public string TypeName { get; set; }
		public Dictionary<string, object> Values { get; set; }
	}
}
