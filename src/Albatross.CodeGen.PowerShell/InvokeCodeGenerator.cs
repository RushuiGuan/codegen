using System.IO;
using System;
using System.Management.Automation;
using System.Text;
using Albatross.CodeGen.Core;
using SimpleInjector;
using System.Collections.Generic;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Invoke, "CodeGenerator")]
	public class InvokeCodeGenerator : PSCmdlet {
		[Parameter(Position = 0, Mandatory =true)]
		[Alias("n")]
		public string Name { get; set; }

		[Parameter(Position = 1, Mandatory = false, ValueFromPipeline = true)]
		[Alias("s")]
		public object Source { get; set; }

		[Parameter(Position = 2, Mandatory = false)]
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
			ICodeGeneratorFactory factory = Ioc.Get<ICodeGeneratorFactory>();
			var meta = factory.Get(Name);

			if (Source is PSObject) { Source = ((PSObject)Source).BaseObject; }
			if (Source == null) { Source = new object(); }
			if(Option == null) { Option = (ICodeGeneratorOption)Activator.CreateInstance(meta.OptionType); }

			IRunCodeGenerator codeGen = Ioc.Get<IRunCodeGenerator>();
			StringBuilder sb = new StringBuilder();
			ICustomCodeSection section = Ioc.Get<ICustomCodeSectionStrategy>().Get(meta.Target);
			if (Output.Exists) {
				using (StreamReader reader = new StreamReader(Output.FullName)) {
					section.Load(reader.ReadToEnd());
				}
			}
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
