using Albatross.CodeGen.CSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen
{
	public class GetReflectionOnlyType {
		public GetReflectionOnlyType() {
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
		}

		private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args) {
			return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
		}

		public Type Get(ObjectType objectType) {
			Assembly asm = Assembly.ReflectionOnlyLoadFrom(objectType.AssemblyLocation);
			return asm.GetType(objectType.ClassName);
		}
	}
}
