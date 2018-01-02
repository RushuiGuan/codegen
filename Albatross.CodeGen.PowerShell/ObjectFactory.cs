using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	public class ObjectFactory : IObjectFactory {
		Container _container;
		public ObjectFactory(Container container) {
			_container = container;
		}

		public T Create<T>() where T : class {
			return _container.GetInstance<T>();
		}
	}
}
