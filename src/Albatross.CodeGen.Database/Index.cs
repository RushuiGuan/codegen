using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public class Index
    {
		public int Object_id { get; set; }
		public string Name { get; set; }
		public int Index_Id { get; set; }
		public int Type { get; set; }
		public bool Is_Unique { get; set; }
		public bool Is_Primary_Key { get; set; }
		public bool Is_Unique_Constraint { get; set; }
	}
}
