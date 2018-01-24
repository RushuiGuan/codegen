using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableDelete : TableQueryGenerator {
		public override string Description => "Default delete statement";
		public override string Name => "table_delete";

		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			return sb.Append($"delete from [{t.Schema}].[{t.Name}]");
		}
	}
}
