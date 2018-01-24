using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Get, "ScenarioLocation")]
    public class GetScenarioLocation:BaseCmdlet<CodeGenSettingFactory> {
		protected override void ProcessRecord() {
			var setting = Handle.Get();
			foreach (var item in setting.ScenarioLocations) {
				WriteObject(item);
			}
		}
	}
}
