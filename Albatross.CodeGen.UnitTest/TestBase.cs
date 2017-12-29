
using Albatross.CodeGen.SqlServer;
using SimpleInjector;

namespace Albatross.CodeGen.UnitTest {
	public class TestBase {

		protected Container GetContainer() {
			Container container = new Container();
			container.Options.AllowOverridingRegistrations = true;
			new Albatross.CodeGen.SqlServer.Pack().RegisterServices(container);
			container.Register<ICodeGeneratorFactory, ContainerControlledCodeGenFactory>(Lifestyle.Singleton);

			return container;
		}

	}
}
