using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Newtonsoft.Json;
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
			if (type == null) {
				return name;
			} else {
				return $"{type.FullName}.{name}";
			}
		}

		public static Type LoadType(this ObjectType objType) {
			Assembly asm = Assembly.ReflectionOnlyLoadFrom(objType.AssemblyLocation);
			return asm.GetType(objType.ClassName);
		}

		public static Step Deserialize(string json) {
			StepDefinition def = JsonConvert.DeserializeObject<StepDefinition>(json);
			Step step = new Step {
				SourceType = Type.GetType(def.SourceType),
				Generator = def.Generator,
			};
			step.Source = JsonConvert.DeserializeObject(def.Source, step.SourceType);
			return step;
		}
		public static string Serialize(this Step step) {
			StepDefinition def = new StepDefinition {
				SourceType = step.SourceType.FullName,
				Source = JsonConvert.SerializeObject(step.Source),
				Generator = step.Generator,
			};
			return JsonConvert.SerializeObject(def);
		}
	}
}
