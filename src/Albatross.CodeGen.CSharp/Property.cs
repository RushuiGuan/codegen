﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class Property {
		public string Name { get; set; }
		public string Type { get; set; }
		public AccessModifier Modifier{ get; set; }
		public bool ReadOnly { get; set; }
	}
}
