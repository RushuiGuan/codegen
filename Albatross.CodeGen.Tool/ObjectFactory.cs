using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Tool {
	public class ObjectFactory : IObjectFactory {
		Container _container;
		public ObjectFactory(Container c) {
			_container = c;
		}


		public T Create<T>() where T:class{
			return _container.GetInstance<T>();
		}

		public object Create(Type type) {
			return _container.GetInstance(type);
		}
	}
}
