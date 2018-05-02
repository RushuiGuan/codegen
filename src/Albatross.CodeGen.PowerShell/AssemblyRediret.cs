using System;
using System.Collections.Generic;
using System.Reflection;

namespace Albatross.CodeGen.PowerShell {
	public class AssemblyRediret {
		Dictionary<string, Assembly> registration = new Dictionary<string, Assembly>(StringComparer.InvariantCultureIgnoreCase);

		public AssemblyRediret() {
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		public void Register<T>() {
			registration[typeof(T).Assembly.GetName().Name] = typeof(T).Assembly;
		}

		private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			var name = new AssemblyName(args.Name).Name;
			Assembly asm;
			if (registration.TryGetValue(name, out asm)) {
				return asm;
			} else {
				return null;
			}
		}
	}
}
