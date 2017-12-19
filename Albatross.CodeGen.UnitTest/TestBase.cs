
using Albatross.CodeGen.SqlServer;
using SimpleInjector;

namespace Albatross.CodeGen.UnitTest {
	public class TestBase {

		protected Container GetContainer() {
			Container container = new Container();
			container.Options.AllowOverridingRegistrations = true;
			new Albatross.CodeGen.SqlServer.Pack().RegisterServices(container);
			new Albatross.CodeGen.Pack().RegisterServices(container);
			return container;
		}

	}
}
