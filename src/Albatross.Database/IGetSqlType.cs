using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.Database
{
    public interface IGetSqlType
    {
		SqlType Get(string name);
    }
}
