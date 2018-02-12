using System.Linq;
using Albatross.CodeGen.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp{
	public class GetReflectionOnlyType : IGetReflectionOnlyType {
		public GetReflectionOnlyType() {
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
		}

		private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args) {
			return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
		}

		public Type Get(ObjectType objectType) {
			string folder = System.IO.Path.GetDirectoryName(objectType.AssemblyLocation);
			if (folder == string.Empty) {
				folder = ".";
			}
			FileInfo asmFile = new FileInfo(objectType.AssemblyLocation);
			Type type = null;
			if (asmFile.Exists) {
				IEnumerable<string> files = Directory.GetFiles(folder, "*.dll", SearchOption.AllDirectories);
				files = files.Union(Directory.GetFiles(folder, "*.exe", SearchOption.AllDirectories));
				Assembly target = null;
				foreach (var file in files) {
					try {
						Assembly asm = Assembly.ReflectionOnlyLoadFrom(file);
						if (string.Equals(asm.Location, asmFile.FullName, StringComparison.InvariantCultureIgnoreCase)) {
							target = asm;
						}
					} catch (Exception) {
					}
				}
				type = target?.GetType(objectType.ClassName, true);
				if (type == null) {
					throw new Exception($"Type {objectType.ClassName} not found in assembly {objectType.AssemblyLocation}!");
				}
			} else {
				throw new Exception($"Assembly {objectType.AssemblyLocation} doesn't exist");
			}
			return type;
		}
	}
}
