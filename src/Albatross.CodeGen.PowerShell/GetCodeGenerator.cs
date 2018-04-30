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
		[Parameter(Position = 0)]
		public string Name { get; set; }

		[Parameter]
		[Alias("t")]
		public string Target { get; set; }

		[Parameter]
		[Alias("c")]
		public string Category { get; set; }


		protected override void ProcessRecord() {
			foreach (var item in Handle.Registrations) {
				if (string.Equals(item.Name, Name, StringComparison.InvariantCultureIgnoreCase)) {
					WriteObject(item);
					return;
				} else if (string.Equals(item.Target, Target, StringComparison.InvariantCultureIgnoreCase)) {
					WriteObject(item);
				} else if (string.Equals(item.Category, Category, StringComparison.InvariantCultureIgnoreCase)) {
					WriteObject(item);
				} else if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Target) && string.IsNullOrEmpty(Category)) {
					WriteObject(item);
				}
			}
		}
	}
}
