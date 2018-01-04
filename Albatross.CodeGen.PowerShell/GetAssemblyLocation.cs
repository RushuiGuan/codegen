using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Get, "AssemblyLocation")]
    public class GetAssemblyLocation:BaseCmdlet<AssemblyLocationRepository> {
		protected override void ProcessRecord() {
			var value = Handle.Get(Handle.Path);
			if (value != null && value.Locations != null) {
				foreach (var item in value.Locations) {
					WriteObject(item);
				}
			}
		}
	}
}
