using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public interface ICreateVariable
    {
		void Create(ICodeGenerator generator, string name, string type);
    }
}
