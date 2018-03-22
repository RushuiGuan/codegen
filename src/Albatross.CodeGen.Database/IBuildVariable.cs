using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	/// <summary>
	/// This interface will generate the sql query for a <see cref="Albatross.Database.Variable"/> object.
	/// </summary>
    public interface IBuildVariable
    {
		StringBuilder Build(StringBuilder sb, Variable variable);
    }
}
