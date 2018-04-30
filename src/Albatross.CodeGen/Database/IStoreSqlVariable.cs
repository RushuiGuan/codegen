using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	/// <summary>
	/// The interface allow the code generator to stored a variable that can be retrieved later by using the <see cref="Albatross.CodeGen.Database.IGetSqlVariable" /> interface.
	/// </summary>
    public interface IStoreSqlVariable
    {
		void Store(object owner, Variable variable);
    }
}
