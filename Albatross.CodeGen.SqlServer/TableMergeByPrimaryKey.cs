
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
	public class TableMergeByPrimaryKey : TableQueryGenerator {
		public override string Name => "table_merge_by_primarykey";
		public override string Description => "Composite: merge statement with the source joined by the primary key columns";

		public override StringBuilder Build(StringBuilder sb, DatabaseObject t, SqlQueryOption options, ICodeGeneratorFactory factory) {
			sb.Append("merge ").EscapeName(t.Schema).Dot().EscapeName(t.Name).Append(" as dst").AppendLine();
			factory.Get<DatabaseObject, SqlQueryOption>("table_merge_select").Build(sb, t, options, factory).Space();
			factory.Get<DatabaseObject, SqlQueryOption>("table_merge_join_by_primarykey").Build(sb, t, options, factory).AppendLine();
			factory.Get<DatabaseObject, SqlQueryOption>("table_merge_update_exclude_primarykey").Build(sb, t, options, factory).AppendLine();
			factory.Get<DatabaseObject, SqlQueryOption>("table_merge_insert").Build(sb, t, options, factory);
			sb.Semicolon();
			return sb;
		}
	}
}
