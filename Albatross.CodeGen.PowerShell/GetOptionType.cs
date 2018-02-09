using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Get, "OptionType")]
	public class GetOptionType : BaseCmdlet<IFactory<OptionType>>{
		[Parameter(ValueFromPipeline =true, Position = 0)]
		public string Name { get; set; }

		protected override void ProcessRecord() {
			foreach (var item in Handle.List()) {
				if (string.IsNullOrEmpty(Name) || Name == item.ObjectType.Name) {
					WriteObject(item);
				}
			}
		}
	}
}
