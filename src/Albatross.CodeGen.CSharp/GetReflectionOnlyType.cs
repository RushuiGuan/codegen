using System.Linq;
using Albatross.CodeGen.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp{
	/// <summary>
	/// This implementation of <see cref="Albatross.CodeGen.CSharp.IGetReflectionOnlyType"/> will try to load all assemblies and executables in the same
	/// folder of the target assembly.  It will load the target assembly last hoping that its dependencies have already been loaded.  It also subscribed
	/// to the ReflectionOnlyAssemblyResolve event on the current domain.  As the last resort, the event handler will try to load the missing dependency by name.
	/// </summary>
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
