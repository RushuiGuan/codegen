using Albatross.Logging.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public abstract class JsonFileRepository<T> where T:class{
		string _path;
		protected ILog _log;
		public abstract string FileName { get; }

		public JsonFileRepository(ILogFactory logFactory) {
			_log = logFactory.Get(this);
			_path = Extension.InitRootFolder() + "//" + FileName + Extension.JsonFileExtension;
		}

		public T Get() {
			if (File.Exists(_path)) {
				try {
					using (StreamReader reader = new StreamReader(_path)) {
						return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
					}
				} catch (Exception err) {
					_log.Error(err);
				}
			}
			return null;
		}

		public bool IsExisting() {
			return File.Exists(_path);
		}
		public void Save(T t) {
			using (StreamWriter writer = new StreamWriter(_path)) {
				string content = JsonConvert.SerializeObject(t);
				writer.Write(content);
			}
		}
	}
}
