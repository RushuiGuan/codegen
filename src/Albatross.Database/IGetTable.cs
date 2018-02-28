using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.Database
{
    public interface IGetTable
    {
		Table Get(Server server, string schema, string name);
    }
}
