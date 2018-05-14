using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "CRUDOperation")]
	public class NewCRUDOption : PSCmdlet {

		[Parameter(Mandatory = true)]
		[Alias("t")]
		public Albatross.Database.Table Table { get; set;}

		[Parameter(Mandatory = true)]
		[Alias("p")]
		public Procedure Procedure{ get; set; }


		protected override void ProcessRecord() {
			var data = new CRUDOperation{
				Table = Table,
				Procedure = Procedure,
			};
			WriteObject(data);
		}
	}
}