using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Invoke, "CodeGenerator")]
	public class InvokeCodeGenerator : BaseCmdlet<ICodeGeneratorFactory> {
		[Parameter(Position = 0)]
		[Alias("n")]
		public string Name { get; set; }

		[Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
		[Alias("s")]
		public object Source { get; set; }

		[Parameter(Position = 2, Mandatory = false)]
		[Alias("o")]
		public object Option { get; set; }

		[Parameter(Position = 3, Mandatory = false)]
		[Alias("t")]
		public FileInfo Output { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

		protected override void ProcessRecord() {
			if (Source is PSObject) { Source = ((PSObject)Source).BaseObject; }
			if (Option is PSObject) { Option = ((PSObject)Option).BaseObject; }
			Type sourceType = Source.GetType();

			IRunCodeGenerator runHandle = base.Factory.Create<IRunCodeGenerator>();
			var meta = Handle.Get(sourceType, Name);
			StringBuilder sb = new StringBuilder();

			runHandle.Run(meta, sb, Source, Option);
			WriteObject(sb.ToString());
			if (Output != null) {
				if (!Output.Exists || Force || this.ShouldContinue("The file already exists, continue and overwrite?", "Warning")) {
					using (var stream = new FileStream(Output.FullName, FileMode.OpenOrCreate)) {
						using (var writer = new StreamWriter(stream)) {
							writer.Write(sb.ToString());
						}
						stream.SetLength(stream.Position);
					}
				}
			}
		}
	}
}
