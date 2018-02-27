using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public class Table
    {
		public string Name { get; set; }
		public int Object_Id { get; set; }
		public int Schema_Id { get; set; }
		public char Type { get; set; }
	}
}
