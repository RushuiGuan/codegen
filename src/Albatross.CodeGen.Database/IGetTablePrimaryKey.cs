using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database
{
    public interface IGetTablePrimaryKey {
		IEnumerable<Column> Get(Server server, string schema, string name);
    }
}
