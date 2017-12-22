using Albatross.CodeGen.Database;
using Albatross.CodeGen.Shell.View;
using Albatross.CodeGen.Shell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public class ViewLocator : IViewLocator {
		Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

		public void Register(Type viewModelType, Type viewType) {
			_registrations[viewModelType] = viewType;
		}

		public Type GetView(Type viewModelType) {
			_registrations.TryGetValue(viewModelType, out Type type);
			return type;
		}
	}
}
