using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsLifecycle.Invoke, "Composite")]
	public class InvokeComposite: BaseCmdlet<IRunCodeGenerator> {

		[Parameter(Position = 0, Mandatory =true, ValueFromPipeline =true)]
		public IComposite Composite { get; set; }

		[Parameter(Position = 1, Mandatory = true)]
		public object Source{ get; set; }

		[Parameter(Position = 2, Mandatory = false)]
		public object Option { get; set; }

		protected override void ProcessRecord() {
			var meta = Composite.GetMeta();
			StringBuilder sb = new StringBuilder();
			if(Source is PSObject) { Source = ((PSObject)Source).BaseObject; }
			if (Option is PSObject) { Option = ((PSObject)Option).BaseObject; }
			Handle.Run(meta, sb, Source, Option);
		}
	}
}
