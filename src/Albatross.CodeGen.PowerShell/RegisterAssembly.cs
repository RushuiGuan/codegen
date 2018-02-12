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
			var srcTypeFactory = Factory.Create<IFactory<SourceType>>();
			var optionTypeFactory = Factory.Create<IFactory<OptionType>>();

			Assembly.Load("Albatross.CodeGen").Register(Handle, srcTypeFactory, optionTypeFactory);
			Assembly.Load("Albatross.CodeGen.CSharp").Register(Handle, srcTypeFactory, optionTypeFactory);
			Assembly.Load("Albatross.CodeGen.Database").Register(Handle, srcTypeFactory, optionTypeFactory);
			Assembly.Load("Albatross.CodeGen.SqlServer").Register(Handle, srcTypeFactory, optionTypeFactory);

			Handle.RegisterStatic();

			if (File.Exists(Location?.FullName)) {
				Assembly asm = Assembly.LoadFile(Location.FullName);
				asm.Register(Handle, base.Factory.Create<IFactory<SourceType>>(), Factory.Create<IFactory<OptionType>>());
			}
		}
	}
}
