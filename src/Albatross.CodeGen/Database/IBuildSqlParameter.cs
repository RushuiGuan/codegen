using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	/// <summary>
	/// This interface will generate the sql query for a <see cref="Albatross.Database.Parameter"/> object.
	/// </summary>
    public interface IBuildSqlParameter {
		StringBuilder Build(StringBuilder sb, Parameter variable);
    }
}
