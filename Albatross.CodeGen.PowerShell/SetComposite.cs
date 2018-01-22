using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Set, "Composite", SupportsShouldProcess =true)]
    public class SetComposite: BaseCmdlet<JsonFileRepository<Composite>>  {
		public SetComposite() {
		}


		[Parameter(Mandatory =true, Position =0, ValueFromPipeline =true)]
		[Alias("c")]
		public Composite Composite{ get; set; }

		[Parameter(Position = 1)]
		[Alias("l")]
		public string Location { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

		protected override void ProcessRecord() {
			Handle.Save(Composite, Location);
		}
	}
}
