using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using Moq;
using SimpleInjector;
using System;

namespace Albatross.CodeGen.UnitTest {
	public class Ioc : IObjectFactory{
		Container container = new Container();
		public T Create<T>() where T : class {
			return container.GetInstance<T>();
		}

		public object Create(Type type) {
			return container.GetInstance(type);
		}

		private Ioc() {
			Setup();
		}


		void Setup() {
			container.Options.AllowOverridingRegistrations = true;
			container.RegisterSingleton<IObjectFactory>(this);
			new Albatross.CodeGen.SimpleInjector .Pack().RegisterServices(container);
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);

			var mock_getTable = new Mock<IGetTable>();
			container.RegisterSingleton<Mock<IGetTable>>(mock_getTable);
			container.RegisterSingleton<IGetTable>(mock_getTable.Object);
		}

		

		public static Lazy<Ioc> _lazy = new Lazy<Ioc>(()=>new Ioc());

		public static Container Container { get { return _lazy.Value.container; } }
	}
}
