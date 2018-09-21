using SimpleInjector;
using System;

namespace Albatross.CodeGen.PowerShell {
	public class Setup {

		private IObjectFactory Run() {
			Container container = new Container();
			new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(container);
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);
			container.Verify();

			return container.GetInstance<IObjectFactory>();
		}

		static Lazy<IObjectFactory> _lazy = new Lazy<IObjectFactory>(() => new Setup().Run());
		public static IObjectFactory Factory { get; private set; } = _lazy.Value;
	}
}
