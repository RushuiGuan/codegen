
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class TableSelectScopedIdentity : TableCodeGenerator {
		IGetTableIdentityColumn _getIDColumn;

		public override string Name => "table_select_scope_identity";
		public override string Description => "Select the Scope_Identity() of the previous insert operation.  Typically used after the insert operation.";

		public TableSelectScopedIdentity(IGetTableIdentityColumn getIDColumn) {
			_getIDColumn = getIDColumn;
		}

		public override StringBuilder Build(StringBuilder sb, Table t, ICodeGeneratorFactory factory) {
			Column identityColumn = _getIDColumn.Get(t);
			if (identityColumn == null) {
				throw new IdentityColumnNotFoundException(t);
			}
			return sb.Append("select scope_identity() as ").EscapeName(identityColumn.Name);
		}
	}
}
