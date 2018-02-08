using Albatross.CodeGen.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp{
	public class GetReflectionOnlyType : IDisposable{
		public GetReflectionOnlyType() {
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
		}

		private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args) {
			try {
				return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
			} catch (FileNotFoundException) {
				string folder = System.IO.Path.GetDirectoryName(args.RequestingAssembly.Location);
				string file = folder + "\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
				return System.Reflection.Assembly.ReflectionOnlyLoadFrom(file);
			}
		}

		public Type Get(ObjectType objectType) {
			System.Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(objectType.AssemblyLocation);
			Assembly asm = Assembly.ReflectionOnlyLoadFrom(objectType.AssemblyLocation);
			return asm.GetType(objectType.ClassName);
		}

		public void Dispose() {
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
		}
	}
}
