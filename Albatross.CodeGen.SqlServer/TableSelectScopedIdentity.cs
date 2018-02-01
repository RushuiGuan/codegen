
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_select_scope_identity", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Select the Scope_Identity() of the previous insert operation.  Typically used after the insert operation.")]
	public class TableSelectScopedIdentity : TableQueryGenerator {
		IGetTableIdentityColumn _getIDColumn;

		public TableSelectScopedIdentity(IGetTableIdentityColumn getIDColumn) {
			_getIDColumn = getIDColumn;
		}

		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			Column identityColumn = _getIDColumn.Get(t);
			if (identityColumn == null) {
				throw new IdentityColumnNotFoundException(t);
			}
			return sb.Append("select scope_identity() as ").EscapeName(identityColumn.Name);
		}
	}
}
