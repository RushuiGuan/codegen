using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Generation {
	public class SqlCodeGenOption : ICodeGeneratorOption {
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

		public bool AllowCustomCode { get; set; }
	}
}
