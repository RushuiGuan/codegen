using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public interface IGetVariable
    {
		IDictionary<string, string> Get(ICodeGenerator generator);
    }
}
