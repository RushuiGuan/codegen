using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Albatross.CodeGen.Core;

namespace Albatross.CodeGen.ClassLoader {
	public class GetAssemblyTypes {
		IServiceProvider provider;
		IEnumerable<string> paths;

		public GetAssemblyTypes(IServiceProvider provider, IEnumerable<string> paths) {
			this.provider = provider;
			this.paths = paths;
		}

		Assembly LoadFromPaths(ResolveEventArgs args) {
			foreach(string item in paths) {
				string name = Path.Combine(new DirectoryInfo(item).FullName, new AssemblyName(args.Name).Name + ".dll");
				if (File.Exists(name)) {
					return Assembly.LoadFrom(name);
				}
			}
			return null;
		}

		public void Get(Assembly asm, string pattern, IEnumerable<string> namespaces, string converterTypeName) {
			var handler = new ResolveEventHandler((obj, asmArgs) => LoadFromPaths(asmArgs));
			AppDomain.CurrentDomain.AssemblyResolve += handler;
			Regex regex = null;

			IConvertObject<Type> converter = provider.GetService(Type.GetType(converterTypeName, true)) as IConvertObject<Type>;
			if (!string.IsNullOrEmpty(pattern)) {
				regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			}
			try {
				List<CSharp.Model.Class> list = new List<CSharp.Model.Class>();
				var writer = new JsonTextWriter(Console.Out);
				JsonSerializer serializer = new JsonSerializer();
				writer.Formatting = Formatting.None;
				writer.WriteStartArray();

				foreach (Type type in asm.GetTypes()) {
					if (!type.IsAnonymousType() && !type.IsInterface && type.IsPublic) {
						bool match = (namespaces?.Count() ?? 0) == 0 || namespaces.Contains(type.Namespace, StringComparer.InvariantCultureIgnoreCase);

						if (match) {
							match = match && (regex == null || regex.IsMatch(type.FullName));
						}

						if (match) {
							var data = converter.Convert(type);
							serializer.Serialize(writer, data);
						}
					}
				}
				writer.WriteEndArray();
				writer.Flush();
			} finally {
				AppDomain.CurrentDomain.AssemblyResolve -= handler;
			}
		}
	}
}