using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;

namespace Albatross.CodeGen.SqlServer
{
	public class StoredProcedureProxy : ICodeGenerator<DatabaseObject, ClassOption> {
		public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }

		public IEnumerable<object> Build(StringBuilder sb, DatabaseObject source, ClassOption option) {
			throw new NotImplementedException();
		}

		public void Configure(object data) { }
	}
}
