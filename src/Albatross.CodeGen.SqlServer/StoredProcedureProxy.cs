using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.Database;

namespace Albatross.CodeGen.SqlServer
{
	/// <summary>
	/// Create a CSharp class from a SQL Stored procedure
	/// </summary>
	public class StoredProcedureProxy : ICodeGenerator<Procedure, ClassOption> {
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }

		public IEnumerable<object> Build(StringBuilder sb, Procedure source, ClassOption option) {
			throw new NotImplementedException();
		}

		public void Configure(object data) { }
	}
}
