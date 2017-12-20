using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Newtonsoft.Json;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public static class Extension {
		public static string GetAssemblyResource(this Type type, string name) {
			using (Stream stream = type.Assembly.GetManifestResourceStream(name)) {
				using (StreamReader reader = new StreamReader(stream)) {
					return reader.ReadToEnd();
				}
			}
		}

		public static string GetConnectionString(this Table table) {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
				DataSource = table.Server.DataSource,
				InitialCatalog = table.Server.InitialCatalog,
				IntegratedSecurity = true
			};
			return builder.ToString();
		}

		public static string GetGeneratorName(this Type type, string name) {
			if (type == typeof(object) || type == null) {
				return name;
			} else {
				return $"{type.FullName}.{name}";
			}
		}
		public static string GetName(this ICodeGenerator codeGenerator) {
			return codeGenerator.SourceType.GetGeneratorName(codeGenerator.Name);
		}

		public static Type LoadType(this ObjectType objType) {
			Assembly asm = Assembly.ReflectionOnlyLoadFrom(objType.AssemblyLocation);
			return asm.GetType(objType.ClassName);
		}

		public static IEnumerable<Registration> FindCodeGenerator(this Container c, Assembly asm) {
			List<Registration> list = new List<Registration>();
			foreach (Type type in asm.GetTypes()) {
				if (typeof(ICodeGenerator).IsAssignableFrom(type) && type.GetCustomAttribute<CodeGeneratorAttribute>() != null) {
					var item = Lifestyle.Singleton.CreateRegistration(type, c);
					list.Add(item);
				}
			}
			return list;
		}

		public static Composite NewSqlTableComposite(string name, string description, params string[] generators) {
			return new Composite {
				SourceType = typeof(Table),
				Name = name,
				Description = description,
				Category = "Sql Server",
				Target = "sql",
				Generators = generators,
				Seperator = "\r'n",
			};
		}
	}
}
