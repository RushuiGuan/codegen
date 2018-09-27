﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class Method {
		public string ReturnType { get; set; }
		public string Name { get; set; }
		public AccessModifier AccessModifier { get; set; }
		public IEnumerable<Parameter> Parameters { get; set; }
		public string Body { get; set; }
		public bool Static { get; set; }
	}
}
