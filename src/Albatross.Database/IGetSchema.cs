using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.Database
{
    public interface IGetSchema
    {
		Schema Get(Server server, string name);
    }
}
