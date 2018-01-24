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
	public class TableMergeDelete : TableQueryGenerator {

		public override string Name => "table_merge_delete";
		public override string Description => "Merage statement delete clause";

		public override StringBuilder Build(StringBuilder sb, Table t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			return sb.Append("when not matched by source then delete");
		}
	}
}
