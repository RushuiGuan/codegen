using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "DatabaseTable")]
	public class NewDatabaseTable : Cmdlet {

		[Parameter(Position = 0, Mandatory = true)]
		public string Name { get; set; }

		[Parameter(Position = 1, Mandatory = true)]
		public string Schema { get; set; }

		[Parameter(Position = 2, Mandatory = true, ValueFromPipeline = true)]
		public Albatross.CodeGen.Database.Server Server { get; set; }


		protected override void ProcessRecord() {
			var table = new Albatross.CodeGen.Database.DatabaseObject {
				Name = Name,
				Schema = Schema,
				Server = Server,
			};
			WriteObject(table);
		}
	}
}