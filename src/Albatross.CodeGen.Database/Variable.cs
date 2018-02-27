using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	/// <summary>
	/// A sql varialbe
	/// </summary>
    public class Variable {
		public string Name { get; set; }
		public string Type { get; set; }
		public int? MaxLength { get; set; }
		public int? Precision { get; set; }
		public int? Scale { get; set; }
	}
}
