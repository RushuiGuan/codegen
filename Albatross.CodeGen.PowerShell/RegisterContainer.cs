using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {

	[Cmdlet(VerbsLifecycle.Register, "Container")]
	public class RegisterContainer : Cmdlet{
		protected override void ProcessRecord() {
			new SetupIoc().Run();
		}
	}
}
