using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public static class Extension {
		public static IViewLocator Register<VM, V>(this IViewLocator locator) {
			locator.Register(typeof(VM), typeof(V));
			return locator;
		}		
	}
}
