using Albatross.CodeGen.Core;
using SimpleInjector;
using System;

namespace Albatross.CodeGen.PowerShell {
	public class Ioc : IObjectFactory{
		private Ioc() {
			container.RegisterSingleton<IObjectFactory>(this);
			new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(container);
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);
			container.Verify();
		}

		Container container = new Container();

		static Lazy<Ioc> lazy = new Lazy<Ioc>(() => new Ioc());
		public static T Get<T>() where T : class {
			return lazy.Value.container.GetInstance<T>();
		}

		public T Create<T>() where T : class {
			return container.GetInstance<T>();
		}

		public object Create(Type type) {
			return container.GetInstance(type);
		}
	}
}
