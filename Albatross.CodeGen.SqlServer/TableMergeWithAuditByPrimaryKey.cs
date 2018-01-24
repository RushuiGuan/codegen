
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
	public class TableMergeWithAuditByPrimaryKey : TableQueryGenerator{
		public override string Name => "table_merge_w_audit_by_primarykey";
		public override string Description => "Composite: merge statement with audit in its select clause and joined by the primary key columns";


		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			sb.Append("merge ").EscapeName(t.Schema).Dot().EscapeName(t.Name).Append(" as dst").AppendLine();
			factory.Get<Table>("table_merge_select_w_audit").Build(sb, t, options, factory).Space();
			factory.Get<Table>("table_merge_join_by_primarykey").Build(sb, t, options, factory).AppendLine();
			factory.Get<Table>("table_merge_update_exclude_primarykey").Build(sb, t, options, factory).AppendLine();
			factory.Get<Table>("table_merge_insert").Build(sb, t, options, factory);
			sb.Semicolon();
			return sb;
		}
	}
}
