using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database {
	public class SqlType {
		public string Name { get; set; }
		public int System_type_id { get; set; }
		public int User_type_id { get; set; }
		public int Schema_id { get; set; }
		public int Max_length { get; set; }
		public int Precision { get; set; }
		public int Scale { get; set; }
		public string Collation_name { get; set; }
		public bool Is_nullable { get; set; }
		public bool Is_user_defined { get; set; }
		public bool Is_table_type { get; set; }
	}
}
