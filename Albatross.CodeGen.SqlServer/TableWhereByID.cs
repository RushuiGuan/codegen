
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableWhereByID : TableCodeGenerator {
		IGetTableIdentityColumn _getIDColumn;
		IGetVariableName _getVariableName;

		public override string Name => "table_where_by_id";
		public override string Description => "Where clause by the identity column";

		public TableWhereByID(IGetTableIdentityColumn getIDColumn, IGetVariableName getVariableName) {
			_getIDColumn = getIDColumn;
			_getVariableName = getVariableName;
		}

		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			Column identityColumn = _getIDColumn.Get(t);
			if (identityColumn == null) {
				throw new IdentityColumnNotFoundException(t);
			}
			string name = _getVariableName.Get(identityColumn.Name);
			sb.AppendLine("where");
			sb.Tab().EscapeName(identityColumn.Name).Append(" = ").Append(name);
			return sb;
		}
	}
}
