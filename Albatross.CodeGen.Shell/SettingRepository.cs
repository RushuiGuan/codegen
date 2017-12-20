using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public class SettingRepository {
		public const string FolderName = "Albatross Code Generator";
		public const string SettingsFileName = "settings.json";

		string _path;

		public SettingRepository() {
			string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + FolderName;
			if (!Directory.Exists(path)){
				Directory.CreateDirectory(path);
			}
			_path = path + SettingsFileName;
		}

		public Settings Get() {
			if (File.Exists(_path)) {
				using (StreamReader reader = new StreamReader(_path)) {
					string content = reader.ReadToEnd();
					return JsonConvert.DeserializeObject<Settings>(content);
				}
			} else {
				return new Settings();
			}
		}
		public void Save(Settings settings) {
			using (StreamWriter writer = new StreamWriter(_path)) {
				string content = JsonConvert.SerializeObject(settings);
				writer.Write(content);
			}
		}
	}
}
