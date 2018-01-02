using Albatross.Logging.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class JsonFileRepository<T> where T:class{
		protected ILog _log;

		public JsonFileRepository(ILogFactory logFactory) {
			_log = logFactory.Get(this);
		}

		public T Get(string name) {
			if (File.Exists(name)) {
				try {
					using (StreamReader reader = new StreamReader(name)) {
						return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
					}
				} catch (Exception err) {
					_log.Error(err);
					throw;
				}
			} else {
				throw new ArgumentException($"File {name} doesn't exist");
			}
		}

		public bool IsExisting(string name) {
			return File.Exists(name);
		}
		public void Save(T t, string name) {
			using (StreamWriter writer = new StreamWriter(name)) {
				string content = JsonConvert.SerializeObject(t);
				writer.Write(content);
			}
		}
		public void Delete(string name) {
			File.Delete(name);
		}
	}
}
