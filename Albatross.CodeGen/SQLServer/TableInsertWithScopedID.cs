namespace Albatross.DbScripting.SqlServer {
	public class TableInsertWithScopedID : CompositeTableQuery{
		public TableInsertWithScopedID() : base("table_insert_w_scoped_identity", "table_insert", "table_select_scope_identity") {
			Description = "Composite: insert statement with the return of the identity column";
		}
	}
}
