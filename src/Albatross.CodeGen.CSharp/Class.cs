using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class Class {
		public AccessModifier AccessModifier { get; set; }
		public string Name { get; set; }
		public string BaseClass { get; set; }
		public string Namespace { get; set; }
		public string Imports { get; set; }

		public IEnumerable<Constructor> Constructors { get; set; }
		public IEnumerable<Property> Properties { get; set; }
		public IEnumerable<Method> Methods { get; set; }

	}
}
