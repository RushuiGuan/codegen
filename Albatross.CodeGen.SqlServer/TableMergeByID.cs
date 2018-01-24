using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeByID : TableQueryGenerator {
		public override string Name => "table_merge_by_id";
		public override string Description => "Composite: merge statement with the source joined by the identity column";


		public override StringBuilder Build(StringBuilder sb, Table t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			sb.Append("merge ").EscapeName(t.Schema).Dot().EscapeName(t.Name).Append(" as dst").AppendLine();
			factory.Get<Table, SqlQueryOption>("table_merge_select").Build(sb, t, options, factory).Space();
			factory.Get<Table, SqlQueryOption>("table_merge_join_by_id").Build(sb, t, options, factory).AppendLine();
			factory.Get<Table, SqlQueryOption>("table_merge_update").Build(sb, t, options, factory).AppendLine();
			factory.Get<Table, SqlQueryOption>("table_merge_insert").Build(sb, t, options, factory);
			sb.Semicolon();
			return sb;
		}

	}
}
