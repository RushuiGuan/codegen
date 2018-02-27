using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public class View
    {
		public string Name { get; set; }
		public int Object_id { get; set; }
		public int Schema_id { get; set; }
		public char Type { get; set; }
	}
}
