using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommunications.Write, "Code")]
	public class WriteCode: BaseCmdlet<ICodeGeneratorFactory> {
		[Parameter(Position = 0)]
		public string Name { get; set; }

		[Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
		public object Source { get; set; }

		[Parameter(Position = 2, Mandatory = false)]
		public object Option { get; set; }


		protected override void ProcessRecord() {
			if (Source is PSObject) {
				Source = ((PSObject)Source).BaseObject;
			}
			Type sourceType = Source.GetType();
			var generator = Handle.Get(sourceType, Name);
			if (Option == null) {
				Option = Activator.CreateInstance(generator.OptionType);
			} else if (Option is PSObject) {
				Option = ((PSObject)Option).BaseObject;
			}

			StringBuilder sb = new StringBuilder();
			generator.Build(sb, Source, Option, Handle);
			WriteObject(sb.ToString());
		}
	}
}
