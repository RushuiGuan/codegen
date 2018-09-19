
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table-select-scope-identity", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Select the Scope_Identity() of the previous insert operation.  Typically used after the insert operation.")]
	public class TableSelectScopedIdentity : TableQueryGenerator {
		IGetTable getTable;

		public TableSelectScopedIdentity(IGetTable getTable) {
			this.getTable = getTable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table t, SqlCodeGenOption options) {
			t = getTable.Get(t.Database, t.Schema, t.Name);
			Column identityColumn = t.IdentityColumn;
			if (identityColumn == null) {
				throw new CodeGenException("Identity Column doesn't exist");
			}
			sb.Append("select scope_identity() as ").EscapeName(identityColumn.Name);
			return new[] { this };
		}
	}
}
