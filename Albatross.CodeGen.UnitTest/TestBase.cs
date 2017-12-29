
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;

namespace Albatross.CodeGen.UnitTest {
	public class TestBase {

		protected Container GetContainer() {
			Container container = new Container();
			container.Options.AllowOverridingRegistrations = true;

			container.RegisterSingleton<IGetTableColumns, GetTableColumns>();
			container.RegisterSingleton<IGetVariableName, GetSqlVariableName>();
			container.RegisterSingleton<IGetTablePrimaryKey, GetTablePrimaryKey>();
			container.RegisterSingleton<IGetTableIdentityColumn, GetTableIdentityColumn>();
			container.RegisterSingleton<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>();
			container.RegisterCollection<BuiltInColumn>(BuiltInColumns.Items);
			container.RegisterSingleton<IBuiltInColumnFactory, BuiltInColumnFactory>();


			container.Register<ICodeGeneratorFactory, ContainerControlledCodeGenFactory>(Lifestyle.Singleton);

			return container;
		}

	}
}
