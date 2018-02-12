using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp
{
    public interface IGetReflectionOnlyType
    {
		Type Get(ObjectType type);
    }
}
