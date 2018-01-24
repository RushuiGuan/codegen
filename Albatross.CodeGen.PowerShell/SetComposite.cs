using System;
using System.Collections.Generic;
using System.IO;
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
		[Alias("f")]
		public FileInfo File { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

		protected override void ProcessRecord() {
			if (!Handle.IsExisting(File.FullName) || Force || this.ShouldContinue("The file already exists, continue and overwrite?", "Warning")) {
				Handle.Save(Composite, File.FullName);
			}
		}
	}
}
