namespace Albatross.CodeGen.SqlServer {
	public class BuiltInComposites {
		public readonly static Composite[] Composites = new Composite[] {
			Extension.NewSqlTableComposite("table_delete_default_by_id", "Delete a sql table row by the identity column", "table_where_by_id"),
			Extension.NewSqlTableComposite("table_delete_default_by_primarykey", "Delete a sql table row by its primary key", "table_delete", "table_where_by_primarykey"), 
			Extension.NewSqlTableComposite("table_insert_w_scoped_identity", "Composite: insert statement with the return of the identity column", "table_insert", "table_select_scope_identity"), 
			Extension.NewSqlTableComposite("table_select_by_id", "Composite: Select statement with the where clause on the identity column", "table_select", "table_where_by_id"), 
			Extension.NewSqlTableComposite("table_select_by_primarykey", "Composite: Select statement with the where clause on the primary key", "table_select", "table_where_by_primarykey"), 
			Extension.NewSqlTableComposite("table_update_by_id", "Update statement with where clause on the identity column", "table_update", "table_where_by_id"), 
			Extension.NewSqlTableComposite("table_update_by_primarykey", "Update statement with where clause on the primary key columns", "table_update_exclude_primarykey", "table_where_by_primarykey"), 
		};

		/*
		public void RegisterServices(Container container) {
			container.RegisterSingleton<IGetTableColumns, GetTableColumns>();
			container.RegisterSingleton<IGetVariableName, GetSqlVariableName>();
			container.RegisterSingleton<IGetTablePrimaryKey, GetTablePrimaryKey>();
			container.RegisterSingleton<IGetTableIdentityColumn, GetTableIdentityColumn>();
			container.RegisterSingleton<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>();

			container.RegisterCollection<BuiltInColumn>(BuiltInColumns.Items);
			container.RegisterSingleton<IBuiltInColumnFactory, BuiltInColumnFactory>();
		}
		*/
	}
}
