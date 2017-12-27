using Albatross.Logging.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public abstract class JsonFolderRepository<T> where T:class{
		string _path;
		ILog _log;
		public abstract string FolderName { get; }

		public JsonFolderRepository(ILogFactory logFactory) {
			_log = logFactory.Get(this);
			_path = Extension.InitRootFolder() + "\\" + FolderName;
			if (!Directory.Exists(_path)){
				Directory.CreateDirectory(_path);
			}
		}

		public string GetPath(string name) {
			return _path = "\\" + name + Extension.JsonFileExtension;
		}

		public IEnumerable<T> Get() {
			List<T> list = new List<T>();
			if (Directory.Exists(_path)) {
				foreach (var file in Directory.GetFiles(_path, "*" + Extension.JsonFileExtension)) {
					try {
						using (StreamReader reader = new StreamReader(file)) {
							T t = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
							list.Add(t);
						}
					} catch (Exception err) {
						_log.Error(err);
					}
				}
			}
			return list;
		}

		public T Get(string name) {
			string path = GetPath(name);
			if (File.Exists(path)) {
				using (StreamReader reader = new StreamReader(path)) {
					return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
				}
			} else {
				return null;
			}
		}
		public bool IsExisting(string name) {
			string path = GetPath(name);
			return File.Exists(path);
		}
		public void Save(T t, Func<T, string> getName) {
			string name = getName(t);
			string path = GetPath(name);
			using (StreamWriter writer = new StreamWriter(path)) {
				string content = JsonConvert.SerializeObject(t);
				writer.Write(content);
			}
		}
	}
}
