using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen {
	public class CodeGenSettingFactory : IFactory<CodeGenSetting> , ISaveFile<CodeGenSetting>{
		public string FileName => "settings.json";
		IGetDefaultRepoFolder getDefaultFolder;
		JsonFileRepository<CodeGenSetting> fileRepo;

		public CodeGenSettingFactory(IGetDefaultRepoFolder getDefaultFolder, JsonFileRepository<CodeGenSetting> fileRepo) {
			this.getDefaultFolder = getDefaultFolder;
			this.fileRepo = fileRepo;
		}

		public CodeGenSetting Get() {
			string path = getDefaultFolder.Get() + "\\" + FileName;
			if (!File.Exists(path)) {
				var file = File.Create(path);
				file.Close();
			}
			var result = fileRepo.Get(path);
			if (result == null) {
				result = new CodeGenSetting {
					AssemblyLocations = new string[0],
					CompositeLocations = new string[0],
					ScenarioLocations = new string[0],
				};
			}
			return result;
		}

		public void Save(CodeGenSetting t) {
			string path = getDefaultFolder.Get() + "\\" + FileName;
			fileRepo.Save(t, path);
		}
	}
}
