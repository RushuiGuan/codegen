﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public class Settings {
		public CodeGeneratorLocations CodeGeneratorLocations { get; set; }
		public Composite[] UserDefinedComposites { get; set; }
	}
}