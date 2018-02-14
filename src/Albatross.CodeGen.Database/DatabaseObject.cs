using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {

	[SourceType("A database object, such as Table, Stored Procedure, View")]
	public class DatabaseObject {
		public string Schema { get; set; }
		public string Name { get; set; }

		public Server Server { get; set; } = new Server();
	}
}
