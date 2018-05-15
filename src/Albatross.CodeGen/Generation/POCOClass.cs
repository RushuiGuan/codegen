using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Generation
{
    public class PocoClass {
		public string Name { get; set; }
		public string Namespace { get; set; }
		public IDictionary<string, Type> Properties { get; set; } 
		public IDictionary<string, string> TypeOverrides { get; set; }
    }
}
