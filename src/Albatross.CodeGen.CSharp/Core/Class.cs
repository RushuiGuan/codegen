using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public class Class {
		public AccessModifier AccessModifier { get; set; }
		public string Name { get; set; }
		public string BaseClass { get; set; }
		public bool Static { get; set; }

		public string Namespace { get; set; }
		public IEnumerable<string> Imports { get; set; }

		public IEnumerable<Constructor> Constructors { get; set; }
		public IEnumerable<Property> Properties { get; set; }
		public IEnumerable<Field> Fields { get; set; }
		public IEnumerable<Method> Methods { get; set; }

	}
}
