using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "StoredProcedure")]
	public class NewStoredProcedure : PSCmdlet {

		[Parameter(Mandatory = true, Position = 0)]
		public string Name { get; set; }

		[Parameter(Mandatory = true, Position = 1)]
		public string Schema{ get; set; }

		[Parameter(Mandatory = true, Position = 2, ValueFromPipeline =true)]
		public Albatross.Database.Database Database{ get; set; }

		[Parameter]
		public string AlterScript { get; set; }

		[Parameter]
		public string CreateScript { get; set; }

		[Parameter]
		public Albatross.Database.Parameter[] Parameters{ get; set; }

		protected override void ProcessRecord() {
			WriteObject(new Albatross.Database.Procedure {
				Database = Database,
				Name = Name,
				Schema = Schema,
				AlterScript = AlterScript,
				CreateScript = CreateScript,
				Parameters = Parameters,
			});
		}
	}
}
