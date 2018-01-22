using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class ScenarioFactory : FileFactory<Scenario> {
		public ScenarioFactory(IFactory<CodeGenSetting> settingFactory, IGetFiles getFiles, JsonFileRepository<Scenario> jsonFile) : base(settingFactory, getFiles, jsonFile) {
		}

		protected override string Extension => "*.scenario";
	}
}
