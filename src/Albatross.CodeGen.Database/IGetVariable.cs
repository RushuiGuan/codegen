using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database{
	/// <summary>
	/// return all the variables created by the creator
	/// </summary>
    public interface IGetVariable
    {
		IEnumerable<Variable> Get(object creator);
    }
}
