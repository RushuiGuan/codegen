using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.Database
{
    public interface IGetConnectionString
    {
		string Get(Server server);
    }
}
