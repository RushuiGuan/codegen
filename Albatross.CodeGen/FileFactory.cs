using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public abstract class FileFactory<T>: IFactory<IEnumerable<T>> where T:class{
		IGetFiles getFiles;
		IFactory<CodeGenSetting> settingFactory;
		JsonFileRepository<T> jsonFile;

		protected abstract string Extension { get; }

		public FileFactory(IFactory<CodeGenSetting> settingFactory, IGetFiles getFiles, JsonFileRepository<T> jsonFile) {
			this.settingFactory = settingFactory;
			this.getFiles = getFiles;
			this.jsonFile = jsonFile;
		}

		public IEnumerable<T> Get() {
			List<T> list = new List<T>();
			var locations = settingFactory.Get().CompositeLocations;
			if (locations != null) {
				foreach (var item in locations) {
					var files = getFiles.Get(item, Extension);
					foreach (var file in files) {
						list.Add(jsonFile.Get(file.FullName));
					}
				}
			}
			return list;
		}
	}
}
