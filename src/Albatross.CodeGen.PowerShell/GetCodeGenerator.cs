using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Get, "CodeGenerator")]
	public class GetCodeGenerator: BaseCmdlet<ICodeGeneratorFactory> {
		[Parameter(Position = 0, ValueFromPipeline = true)]
		public string Name { get; set; }

		protected override void ProcessRecord() {
			if (string.IsNullOrEmpty(Name)) {
				foreach (var item in Handle.Registrations) {
					WriteObject(item);
				}
			} else {
				foreach (var item in Handle.Registrations) {
					if (string.Equals(item.Name, Name, StringComparison.InvariantCultureIgnoreCase)) {
						WriteObject(item);
					}
				}
			}
		}
	}
}
