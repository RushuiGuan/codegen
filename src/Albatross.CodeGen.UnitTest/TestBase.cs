
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;

namespace Albatross.CodeGen.UnitTest {
	public class TestBase {

		protected Container GetContainer() {
			Container container = new Container();
			container.Options.AllowOverridingRegistrations = true;

			container.RegisterSingleton<IGetTableColumn, GetTableColumn>();
			container.RegisterSingleton<ICreateVariableName, CreateSqlVariableName>();
			container.RegisterSingleton<IGetTablePrimaryKey, GetTablePrimaryKey>();
			container.RegisterSingleton<IGetTableIdentityColumn, GetTableIdentityColumn>();
			container.RegisterSingleton<IColumnSqlTypeBuilder, BuildSqlType>();

			return container;
		}

	}
}
