using System;
using SimpleInjector;

namespace Albatross.CodeGen.PowerShell {
	public class ObjectFactory : IObjectFactory {
		Container _container;
		public ObjectFactory(Container container) {
			_container = container;
		}

		public T Create<T>() where T : class {
			return _container.GetInstance<T>();
		}

		public object Create(Type type) {
			return _container.GetInstance(type);
		}
	}
}
