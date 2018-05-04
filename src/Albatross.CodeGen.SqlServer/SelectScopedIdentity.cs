using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.select.identity", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Select the Scope_Identity() of the previous insert operation.  Typically used after the insert operation.")]
	public class SelectScopedIdentity : TableQueryGenerator {
		IGetTable getTable;

		public SelectScopedIdentity(IGetTable getTable) {
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
