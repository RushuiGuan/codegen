using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Generation;
using Albatross.CodeGen.SqlServer;
using System.IO;
using System.Management.Automation;
using System.Reflection;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Register, "Assembly")]
	public class RegisterAssembly : BaseCmdlet<IConfigurableCodeGenFactory> {
		[Parameter(Position = 0, ValueFromPipeline = true)]
		public FileInfo Location { get; set; }

		protected override void ProcessRecord() {
			typeof(ICodeGeneratorFactory).Assembly.Register(Handle);
			typeof(BuildSqlType).Assembly.Register(Handle);

			Handle.RegisterStatic();

			if (File.Exists(Location?.FullName)) {
				Assembly asm = Assembly.LoadFile(Location.FullName);
				asm.Register(Handle);
			}
		}
	}
}
