using Albatross.Logging.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public abstract class DefaultFolderRepository<T> where T:class{
		ILog _log;
		public string Path { get; }
		public abstract string FileExtension { get; }

		public DefaultFolderRepository(ILogFactory logFactory, IGetDefaultRepoFolder getDefaultFolder) {
			_log = logFactory.Get(this);
			Path = getDefaultFolder.Get();
			if (!Directory.Exists(Path)){
				Directory.CreateDirectory(Path);
			}
		}

		public string GetPath(string name) {
			return Path + "\\" + name + FileExtension;
		}

		public IEnumerable<T> List() {
			List<T> list = new List<T>();
			if (Directory.Exists(Path)) {
				foreach (var file in Directory.GetFiles(Path, "*" + FileExtension)) {
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
		public void Save(T t, string name) {
			if (string.IsNullOrEmpty(name)) { throw new IOException("Missing Name"); }
			string path = GetPath(name);
			using (StreamWriter writer = new StreamWriter(path)) {
				string content = JsonConvert.SerializeObject(t);
				writer.Write(content);
			}
		}
	}
}
