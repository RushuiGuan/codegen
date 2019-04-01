using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Albatross.CodeGen.TypeScript.Conversion;
using Albatross.CodeGen.Core;

namespace Albatross.CodeGen.ClassLoader
{
    public class GetAssemblyTypes
    {
        IConvertObject<Type, CSharp.Model.Class> convertToCSharpClass;
        IConvertObject<Type, TypeScript.Model.Class> convertToTypeScriptClass;

        public GetAssemblyTypes(IConvertObject<Type, CSharp.Model.Class> convertToCSharpClass, IConvertObject<Type, TypeScript.Model.Class> convertToTypeScriptClass) {
            this.convertToCSharpClass = convertToCSharpClass;
            this.convertToTypeScriptClass = convertToTypeScriptClass;
        }

        static Assembly LoadFromSameFolder(Assembly assembly, ResolveEventArgs args)
        {
            string assemblyPath = Path.Combine(new FileInfo(assembly.Location).DirectoryName, new AssemblyName(args.Name).Name + ".dll");
            if (!File.Exists(assemblyPath)) return null;
            assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }

        public void Get(Assembly asm, string pattern, IEnumerable<string> namespaces, string converter)
        {
            var handler = new ResolveEventHandler((obj, asmArgs) => LoadFromSameFolder(asm, asmArgs));
            AppDomain.CurrentDomain.AssemblyResolve += handler;
            Regex regex = null;
            IConvertObject<Type> handle = convertToCSharpClass;
            switch (converter)
            {
                case Constant.CSharpConverter:
                    handle = convertToCSharpClass;
                    break;
                case Constant.TypeScriptConverter:
                    handle = convertToTypeScriptClass;
                    break;
                default:
                    throw new Exception("Unsupported converted: " + converter);
            }

            if (!string.IsNullOrEmpty(pattern)) {
                regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            }
            try
            {
                List<CSharp.Model.Class> list = new List<CSharp.Model.Class>();
                var writer = new JsonTextWriter(Console.Out);
                JsonSerializer serializer = new JsonSerializer();
                writer.Formatting = Formatting.None;
                writer.WriteStartArray();

                foreach (Type type in asm.GetTypes())
                {
					if (!type.IsAnonymousType() && !type.IsInterface && type.IsPublic) {
						bool match = (namespaces?.Count() ?? 0) == 0 || namespaces.Contains(type.Namespace, StringComparer.InvariantCultureIgnoreCase);

						if (match) {
							match = match && (regex == null || regex.IsMatch(type.FullName));
						}

						if (match) {
							var data = handle.Convert(type);
							serializer.Serialize(writer, data);
						}
					}
                }
                writer.WriteEndArray();
                writer.Flush();
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= handler;
            }
        }
    }
}