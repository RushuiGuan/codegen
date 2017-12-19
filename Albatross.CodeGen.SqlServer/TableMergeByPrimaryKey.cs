
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class TableMergeByPrimaryKey : TableCodeGenerator {
		public override string Name => "table_merge_by_primarykey";
		public override string Description => "Composite: merge statement with the source joined by the primary key columns";

		public override StringBuilder Build(StringBuilder sb, Table t, ICodeGeneratorFactory factory) {
			sb.Append("merge ").EscapeName(t.Schema).Dot().EscapeName(t.Name).Append(" as dst").AppendLine();
			factory.Get<Table>("table_merge_select").Build(sb, t, factory).Space();
			factory.Get<Table>("table_merge_join_by_primarykey").Build(sb, t, factory).AppendLine();
			factory.Get<Table>("table_merge_update_exclude_primarykey").Build(sb, t, factory).AppendLine();
			factory.Get<Table>("table_merge_insert").Build(sb, t, factory);
			sb.Semicolon();
			return sb;
		}
	}
}
