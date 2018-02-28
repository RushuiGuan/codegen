using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.Database
{
	/// <summary>
	/// A sql varialbe
	/// </summary>
    public class Variable {
		public string Name { get; set; }
		public SqlType Type { get; set; }
	}
}
