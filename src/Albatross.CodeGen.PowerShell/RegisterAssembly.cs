using Albatross.CodeGen.Core;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;
using System.IO;
using System.Management.Automation;
using System.Reflection;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Register, "Assembly")]
	public class RegisterAssembly : PSCmdlet{
		[Parameter(Position = 0, ValueFromPipeline = true)]
		public FileInfo Location { get; set; }

		protected override void BeginProcessing() {
			base.BeginProcessing();
			new AssemblyRediret().Register<Container>();
		}

		protected override void ProcessRecord() {
			IConfigurableCodeGenFactory factory = Ioc.Get<IConfigurableCodeGenFactory>();

			typeof(ICodeGeneratorFactory).Assembly.Register(factory);
			typeof(RenderSqlType).Assembly.Register(factory);
			factory.RegisterStatic();

			if (File.Exists(Location?.FullName)) {
				Assembly asm = Assembly.LoadFile(Location.FullName);
				asm.Register(factory);
			}
		}
	}
}
