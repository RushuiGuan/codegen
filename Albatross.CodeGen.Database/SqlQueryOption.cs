using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public class SqlQueryOption
    {
		public bool ExcludePrimaryKey { get; set; } = true;
		public Dictionary<string, Variable> Variables { get; } = new Dictionary<string, Variable>(StringComparer.InvariantCultureIgnoreCase);
    }
}
