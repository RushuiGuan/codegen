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
	[Cmdlet(VerbsLifecycle.Invoke, "CSharpClassGenerator")]
	public class InvokeCSharpClassGenerator : BaseCmdlet<Albatross.CodeGen.CSharp.Writer.WriteCSharpClass> {
		[Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
		[Alias("c")]
		public Albatross.CodeGen.CSharp.Model.Class Class { get; set; }

		[Parameter(Position = 3, Mandatory = false)]
		[Alias("t")]
		public FileInfo Output { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

        StreamWriter writer;
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (Output?.Exists == false || Force || this.ShouldContinue("The file already exists, continue and overwrite?", "Warning"))
            {
                writer = new StreamWriter(new FileStream(Output.FullName, FileMode.OpenOrCreate));
            }
        }
        protected override void ProcessRecord() {
			EntryObject.Run(writer, Class);
			EntryObject.Run(Console.Out, Class);
		}
        protected override void EndProcessing()
        {
			writer.Flush();
			writer.BaseStream.SetLength(writer.BaseStream.Position);
			writer.Close();

			base.EndProcessing();
        }

		protected override void RegisterContainer(ContainerBuilder builder) {
			new Pack().Register(builder);
		}
	}
}
