using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Register, "Assembly")]
	public class RegisterAssembly : BaseCmdlet<IConfigurableCodeGenFactory> {
		[Parameter(Mandatory =true, Position = 0, ValueFromPipeline = true)]
		public FileInfo Location { get; set; }

		protected override void ProcessRecord() {
			Assembly asm = Assembly.LoadFile(Location.FullName);
			Handle.Register(asm);
		}
	}
}
