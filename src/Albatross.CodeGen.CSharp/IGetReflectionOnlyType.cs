using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp{
	/// <summary>
	/// The interface will perform a reflection only load on the input type parameter.  It requires a specific implementation because .NET will only
	/// load the specified assembly and will not load any dependencies automatically.
	/// </summary>
    public interface IGetReflectionOnlyType
    {
		Type Get(ObjectType type);
    }
}
