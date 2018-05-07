using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsData.Publish, "StoredProcedure")]
	public class PublishStoredProcedure : PSCmdlet {
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		[Alias("p")]
		public Procedure Procedure { get; set; }

		protected override void ProcessRecord() {
			Ioc.Get<IDeployProcedure>().Deploy(Procedure);
		}
	}
}
