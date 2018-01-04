using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Register, "CodeGenerators")]
	public class RegisterCodeGenerators : BaseCmdlet<IConfigurableCodeGenFactory> {
		protected override void ProcessRecord() {
			Handle.Register();
		}
	}
}
