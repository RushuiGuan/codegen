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
	public class StoredProcedureProxy : CodeGeneratorBase<Procedure, ClassOption> {
		public override IEnumerable<object> Build(StringBuilder sb, Procedure source, ClassOption option) {
			throw new NotImplementedException();
		}
	}
}
