using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class JsonFileRepository<T> where T:class{
		public T Get(string name) {
			using (StreamReader reader = new StreamReader(name)) {
				return JsonConvert.DeserializeObject<T>(reader.ReadToEnd(), Setting);
			}
		}

		public JsonSerializerSettings Setting { get; private set; } = new JsonSerializerSettings {
			 TypeNameHandling = TypeNameHandling.Objects,
		};

		public bool IsExisting(string name) {
			return File.Exists(name);
		}
		public void Save(T t, string name) {
			using (StreamWriter writer = new StreamWriter(name)) {
				string content = JsonConvert.SerializeObject(t, Setting);
				writer.Write(content);
			}
		}
		public void Delete(string name) {
			File.Delete(name);
		}
	}
}
