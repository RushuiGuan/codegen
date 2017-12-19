using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Reflection;

namespace Albatross.CodeGen.SqlServer {
	public class Pack : SimpleInjector.Packaging.IPackage {

		public void RegisterServices(Container container) {

			List<Registration> registrations = new List<Registration>();
			foreach (Type type in this.GetType().Assembly.GetTypes()) {
				if (typeof(ICodeGenerator).IsAssignableFrom(type) && type != typeof(TableCodeGenerator) && type != typeof(TableCompositeGenerator)) {
					var item = Lifestyle.Singleton.CreateRegistration(type, container);
					registrations.Add(item);
				}
			}

			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_delete_default_by_id", null, "table_where_by_id"), container));
			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_delete_default_by_primarykey", null, "table_delete", "table_where_by_primarykey"), container));
			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_insert_w_scoped_identity", "Composite: insert statement with the return of the identity column", "table_insert", "table_select_scope_identity"), container));
			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_select_by_id", "Composite: Select statement with the where clause on the identity column", "table_select", "table_where_by_id"), container));
			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_select_by_primarykey", "Composite: Select statement with the where clause on the primary key", "table_select", "table_where_by_primarykey"), container));
			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_update_by_id", "Update statement with where clause on the identity column", "table_update", "table_where_by_id"), container));
			registrations.Add(Lifestyle.Singleton.CreateRegistration<ICodeGenerator>(() => new TableCompositeGenerator("table_update_by_primarykey", "Update statement with where clause on the primary key columns", "table_update_exclude_primarykey", "table_where_by_primarykey"), container));

			container.RegisterCollection<ICodeGenerator>(registrations);

			container.RegisterSingleton<IGetTableColumns, GetTableColumns>();
			container.RegisterSingleton<IGetVariableName, GetSqlVariableName>();
			container.RegisterSingleton<IGetTablePrimaryKey, GetTablePrimaryKey>();
			container.RegisterSingleton<IGetTableIdentityColumn, GetTableIdentityColumn>();
			container.RegisterSingleton<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>();

			container.RegisterCollection<BuiltInColumn>(BuiltInColumns.Items);
			container.RegisterSingleton<IBuiltInColumnFactory, BuiltInColumnFactory>();
		}
	}
}
