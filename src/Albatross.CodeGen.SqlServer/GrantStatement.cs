using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.grant", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Sql grant statement")]
	public class GrantStatement : ICodeGenerator<object, SqlCodeGenOption> {
		public event Func<StringBuilder, IEnumerable<object>> Yield;

		public IEnumerable<object> Build(StringBuilder sb, object source, SqlCodeGenOption option) {

			return new object[] { this };
		}

		public IEnumerable<object> Build(StringBuilder sb, object source, object option) {
			return this.ValidateNBuild(sb, source, option);
		}

		public void Configure(object data) {
		}
	}
}
