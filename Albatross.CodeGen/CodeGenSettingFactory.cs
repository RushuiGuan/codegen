using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen {
	public class CodeGenSettingFactory : IFactory<CodeGenSetting> {
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
			return fileRepo.Get(path);
		}
	}
}
