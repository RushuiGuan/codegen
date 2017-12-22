using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public interface IViewLocator {
		void Register(Type viewModelType, Type viewType);
		Type GetView(Type viewModelType);
	}
}
