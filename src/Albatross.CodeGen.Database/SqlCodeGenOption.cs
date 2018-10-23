using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database {
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
		/// Specify arbitrary sql variables. />
		/// </summary>
		public IEnumerable<Variable> Variables { get; set; } = new Variable[0];

		/// <summary>
		/// Specify arbitrary sql parameters. />
		/// </summary>
		public IEnumerable<Parameter> Parameters { get; set; } = new Parameter[0];

		/// <summary>
		/// Replace certain variables using an expression
		/// </summary>
		public Dictionary<string, string> Expressions { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
	}
}
