using Albatross.Database;
using System.Collections.Generic;

namespace Albatross.CodeGen.Database {
	/// <summary>
	/// The interface will retrieve all <see cref="Albatross.Database.Variable">Variables</see> stored by the specified owner />.  This interface works with <see cref="Albatross.CodeGen.Database.IStoreVariable"/>
	/// </summary>
	public interface IGetVariable
    {
		IEnumerable<Variable> Get(object owner);
    }
}
