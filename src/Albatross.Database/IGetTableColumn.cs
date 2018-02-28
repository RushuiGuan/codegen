using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.Database {
	public interface IGetTableColumn {
		IEnumerable<Column> Get(Server server, string schema, string name);
	}
}
