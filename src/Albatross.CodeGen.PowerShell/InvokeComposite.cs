using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.PowerShell.Transformation;
using SimpleInjector;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Invoke, "Composite")]
	public class InvokeComposite: PSCmdlet{
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
		[Array2Branch]
		[Alias("b")]
		public Branch Branch { get; set; }

		[Parameter(Position = 1, Mandatory = false)]
		[Alias("s")]
		public object Source{ get; set; }

		[Parameter(Position = 2, Mandatory = true)]
		[Alias("o")]
		public ICodeGeneratorOption Option { get; set; }

		[Parameter(Position = 3, Mandatory = false)]
		[Alias("t")]
		public FileInfo Output { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

		protected override void BeginProcessing() {
			base.BeginProcessing();
			new AssemblyRediret().Register<Container>();
		}

		protected override void ProcessRecord() {
			IRunCodeGenerator codeGen = Ioc.Get<IRunCodeGenerator>();

			StringBuilder sb = new StringBuilder();
			if(Source is PSObject) { Source = ((PSObject)Source).BaseObject; }
			if(Source == null) { Source = new object(); }

			Composite c = new Composite(Source.GetType(), Option.GetType()) {
				Branch = Branch,
			};
			var meta = c.GetMeta();
			codeGen.Run(meta, sb, Source, Option);
			WriteObject(sb.ToString());
			if (Output != null) {
				if (!Output.Exists || Force || this.ShouldContinue("The file already exists, continue and overwrite?", "Warning")) {
					using (var stream = new FileStream(Output.FullName, FileMode.OpenOrCreate)) {
						using (var writer = new StreamWriter(stream)) {
							writer.Write(sb.ToString());
							writer.Flush();
							stream.SetLength(stream.Position);
						}
					}
				}
			}
		}
	}
}
