using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core {
	public interface IObjectFactory {
		T Create<T>() where T:class;
		object Create(Type type);
	}
}
