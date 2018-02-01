using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
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
			CodeGenerator meta = Handle.GetMetadata(sourceType, Name);
			if (Option is PSObject) {
				Option = ((PSObject)Option).BaseObject;
			}

			if (Option == null) {
				Option = Activator.CreateInstance(meta.SourceType);
			} else {
				if (meta.SourceType != Option.GetType()) { throw new InvalidOptionTypeException(); }
			}

			StringBuilder sb = new StringBuilder();
			MethodInfo method = generator.GetType().GetMethod(nameof(ICodeGenerator<object, object>.Build));
			method.Invoke(generator, new object[] { sb, Source, Option, Handle });
			WriteObject(sb.ToString());
		}
	}
}
