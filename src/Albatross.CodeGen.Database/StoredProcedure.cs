using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	[SourceType("Database stored procedure")]
	public class StoredProcedure {
		public Server Server { get; set; }
		public string Schema { get; set; }
		public string Name { get; set; }
	}
}
