using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Get, "Composite")]
	public class GetComposite : BaseCmdlet<IFactory<IEnumerable<Composite>>> {
		[Parameter(Position = 0)]
		[Alias("n")]
		public string Name { get; set; }

		protected override void ProcessRecord() {
			var items = Handle.Get();
			foreach (var item in items) {
				if (string.IsNullOrEmpty(Name) || string.Equals(Name, item.Name, StringComparison.InvariantCultureIgnoreCase)) {
					WriteObject(item);
					if (!string.IsNullOrEmpty(Name)) {
						return;
					}
				}
			}
		}
	}
}
