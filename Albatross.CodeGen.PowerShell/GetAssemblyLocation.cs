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
			var setting = Handle.Get();
			WriteObject(System.IO.Path.GetDirectoryName(typeof(CodeGenSettingFactory).Assembly.Location));
			foreach (var item in setting.AssemblyLocations) {
				WriteObject(item);
			}
		}
	}
}
