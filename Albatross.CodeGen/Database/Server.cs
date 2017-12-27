using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	[SourceType]
	public class Server {
		public string DataSource { get; set; }
		public string InitialCatalog { get; set; }
	}
}
