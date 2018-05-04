using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	/// <summary>
	/// This interface will generate the sql for a <see cref="Albatross.Database.Parameter"/> object.
	/// </summary>
    public interface IRenderSqlParameter {
		StringBuilder Render(StringBuilder sb, Parameter variable);
    }
}
