using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen{
	[OptionType("C# Class Option")]
	public class ClassOption {
		public ClassOption() {
			BaseClass = "System.Object";
			Imports = new string[0];
			AccessModifier = "public";
			Namespace = "GeneratedCode";
			Constructors = new string[0];
		}


		public string Name { get; set; }
		public string Prefix { get; set; }
		public string Postfix { get; set; }

		public string BaseClass { get; set; }
		public IEnumerable<string> Imports { get; set; }
		public string AccessModifier { get; set; }
		public string Namespace { get; set; }
		public IEnumerable<string> Constructors { get; set; }
	}
}
