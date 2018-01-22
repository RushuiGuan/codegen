using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Set, "AssemblyLocation")]
    public class SetAssemblyLocation : BaseCmdlet<CodeGenSettingFactory> {

		[Parameter(Position =0, ValueFromPipeline =true)]
		public DirectoryInfo[] Locations { get; set; }

		protected override void ProcessRecord() {
			AssemblyLocationSetting setting = new AssemblyLocationSetting();
			if (Locations != null) {
				setting.Locations = from item in Locations select item.FullName;
			}
			Handle.Save(setting, Handle.Path);
		}
	}
}
