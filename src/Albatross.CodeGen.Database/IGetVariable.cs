using Albatross.Database;
using System.Collections.Generic;

namespace Albatross.CodeGen.Database {
	/// <summary>
	/// return all the variables created by the creator
	/// </summary>
	public interface IGetVariable
    {
		IEnumerable<Variable> Get(object creator);
    }
}
