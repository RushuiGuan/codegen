using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public class CompositeFactory : FileFactory<Composite> {
		public CompositeFactory(IFactory<CodeGenSetting> settingFactory, IGetFiles getFiles, JsonFileRepository<Composite> jsonFile) : base(settingFactory, getFiles, jsonFile) {
		}

		protected override string Extension => ".composite";
	}
}
