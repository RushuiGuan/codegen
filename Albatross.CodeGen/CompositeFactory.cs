using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public class CompositeFactory : FileFactory<IComposite> {
		public CompositeFactory(IFactory<CodeGenSetting> settingFactory, IGetFiles getFiles, JsonFileRepository<IComposite> jsonFile) : base(settingFactory, getFiles, jsonFile) {
		}

		protected override string Extension => ".composite";
	}
}
