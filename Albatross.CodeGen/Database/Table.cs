using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	public class Table {
		public string Schema { get; set; }
		public string Name { get; set; }

		public Server Server { get; set; } = new Server();
	}
}
