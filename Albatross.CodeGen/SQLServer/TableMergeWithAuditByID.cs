using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeWithAuditByID : ITableBasedQuery {
		public string Scenario => "table_merge_w_audit_by_id";
		public string Description => "Composite: merge statement with audit in its select clause and joined by the identity column";


		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			sb.Append("merge ").EscapeName(schema).Dot().EscapeName(table).Append(" as dst").AppendLine();
			factory.Get("table_merge_select_w_audit").Build(schema, table, factory, sb, @params).Space();
			factory.Get("table_merge_join_by_id").Build(schema, table, factory, sb, @params).AppendLine();
			factory.Get("table_merge_update").Build(schema, table, factory, sb, @params).AppendLine();
			factory.Get("table_merge_insert").Build(schema, table, factory, sb, @params);
			sb.Semicolon();
			return sb;
		}
	}
}
