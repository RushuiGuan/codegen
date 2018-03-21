using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database {
	[OptionType("Sql Code Generation Options")]
	public class SqlCodeGenOption {
		/// <summary>
		/// Option to exclude primary key for certain operations such as generating an update statement
		/// </summary>
		public bool ExcludePrimaryKey { get; set; } = true;

		/// <summary>
		/// where clause filter options
		/// </summary>
		public FilterOption Filter { get; set; }

		/// <summary>
		/// Specify the name of the generated sql object
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Specify the schema of the generated sql object
		/// </summary>
		public string Schema { get; set; }

		/// <summary>
		/// Specify arbitrary sql variables.  the key is the variable name and should be prefixed with @.  The value should be a valid sql data type.
		/// </summary>
		public IEnumerable<Variable> Variables { get; } = new Variable[0];

		public Dictionary<string, string> Expressions { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
	}
}
