
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
	public class TableMergeWithAuditByID : TableQueryGenerator {

		public override string Name => "table_merge_w_audit_by_id";
		public override string Description => "Composite: merge statement with audit in its select clause and joined by the identity column";

		public override StringBuilder Build(StringBuilder sb, Table t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			sb.Append("merge ").EscapeName(t.Schema).Dot().EscapeName(t.Name).Append(" as dst").AppendLine();
			factory.Get<Table, SqlQueryOption>("table_merge_select_w_audit").Build(sb, t, options, factory).Space();
			factory.Get<Table, SqlQueryOption>("table_merge_join_by_id").Build(sb, t, options, factory).AppendLine();
			factory.Get<Table, SqlQueryOption>("table_merge_update").Build(sb, t, options, factory).AppendLine();
			factory.Get<Table, SqlQueryOption>("table_merge_insert").Build(sb, t, options, factory);
			sb.Semicolon();
			return sb;
		}
	}
}
