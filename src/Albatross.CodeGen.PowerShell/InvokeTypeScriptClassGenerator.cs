using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Albatross.CodeGen.Autofac;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Invoke, "TypeScriptClassGenerator")]
	public class InvokeTypeScriptClassGenerator : BaseCmdlet<Albatross.CodeGen.TypeScript.Writer.WriteClass> {
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
		[Alias("c")]
		public Albatross.CodeGen.TypeScript.Model.Class Class { get; set; }

		[Parameter(Position = 1, Mandatory = false)]
		[Alias("t")]
		public FileInfo Output { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

        StreamWriter writer;
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
			if (Output != null) {
				if (!Output.Exists || Force || this.ShouldContinue("The file already exists, continue and overwrite?", "Warning")) {
					writer = new StreamWriter(new FileStream(Output.FullName, FileMode.OpenOrCreate));
				}
			}
        }
        protected override void ProcessRecord() {
			if (writer != null) {
				EntryObject.Run(writer, Class);
				writer.WriteLine();
			}
			EntryObject.Run(Console.Out, Class);
			Console.WriteLine();
		}
        protected override void EndProcessing()
        {
			if (writer != null) {
				writer.Flush();
				writer.BaseStream.SetLength(writer.BaseStream.Position);
				writer.Close();
			}

			base.EndProcessing();
        }

		protected override void RegisterContainer(ContainerBuilder builder) {
			new Pack().Register(builder);
		}
	}
}
