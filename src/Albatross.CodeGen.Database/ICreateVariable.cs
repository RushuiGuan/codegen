using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public interface ICreateVariable
    {
		void Create(object creator, string name, string type);
    }
}
