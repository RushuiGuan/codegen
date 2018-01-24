using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
	[OptionType("Sql Query Options")]
    public class SqlQueryOption
    {
		public bool ExcludePrimaryKey { get; set; } = true;
		public Dictionary<string, string> Variables { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
		public Dictionary<string, string> Expressions { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
	}
}
