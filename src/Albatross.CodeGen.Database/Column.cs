using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	[SourceType("Sql server table or view column")]
	public class Column {
		public int Object_Id { get; set; }
		public string Name { get; set; }
		public int Column_Id { get; set; }
		public int System_Type_Id { get; set; }
		public int User_Type_Id { get; set; }
		public int Max_length { get; set; }
		public int Precision { get; set; }
		public int Scale { get; set; }
		public bool Is_Nullable { get; set; }
		public bool Is_Ansi_Padded { get; set; }
		public bool Is_RowGuidCol { get; set; }
		public bool Is_Identity { get; set; }
		public bool Is_Computed { get; set; }
		public bool Is_Filestream { get; set; }
	}
}
