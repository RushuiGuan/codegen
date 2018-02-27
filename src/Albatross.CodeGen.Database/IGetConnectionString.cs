using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public interface IGetConnectionString
    {
		string Get(Server server);
    }
}
