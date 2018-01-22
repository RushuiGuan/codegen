using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Get, "AssemblyLocation")]
    public class GetAssemblyLocation:BaseCmdlet<CodeGenSettingFactory> {
		protected override void ProcessRecord() {
			var value = Handle.Get();
			if (value != null && value.AssemblyLocations != null) {
				foreach (var item in value.AssemblyLocations) {
					WriteObject(item);
				}
			}
		}
	}
}
