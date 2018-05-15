using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Start, "CRUDCreateOperation")]
	public class RunCRUDCreateOperation : PSCmdlet {
		[Parameter(Mandatory =true, ValueFromPipeline =true, Position =0)]
		public CRUDProjectOptions Options { get; set; }

		protected override void ProcessRecord() {
		}
	}
}
