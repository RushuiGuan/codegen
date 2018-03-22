using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
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
		[Parameter(Position = 0, ValueFromPipeline = true)]
		public FileInfo Location { get; set; }

		protected override void ProcessRecord() {
			typeof(Albatross.CodeGen.ICodeGeneratorFactory).Assembly.Register(Handle);
			typeof(Albatross.CodeGen.CSharp.ClassGenerator<object>).Assembly.Register(Handle);
			typeof(Albatross.CodeGen.Database.SqlCodeGenOption).Assembly.Register(Handle);
			typeof(Albatross.CodeGen.SqlServer.BuildSqlType).Assembly.Register(Handle);

			Handle.RegisterStatic();

			if (File.Exists(Location?.FullName)) {
				Assembly asm = Assembly.LoadFile(Location.FullName);
				asm.Register(Handle);
			}
		}
	}
}
