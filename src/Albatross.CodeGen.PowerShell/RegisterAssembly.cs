using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Conversion;
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
			Handle.RegisterAssembly(typeof(Albatross.CodeGen.ICodeGeneratorFactory).Assembly);
			Handle.RegisterAssembly(typeof(ConvertStoredProcedureToDapperClass).Assembly);
			Handle.RegisterAssembly(typeof(Albatross.CodeGen.Database.SqlCodeGenOption).Assembly);
			Handle.RegisterAssembly(typeof(Albatross.CodeGen.SqlServer.BuildSqlType).Assembly);

            if (File.Exists(Location?.FullName)) {
				Assembly asm = Assembly.LoadFile(Location.FullName);
				Handle.RegisterAssembly(asm);
			}
		}
	}
}
