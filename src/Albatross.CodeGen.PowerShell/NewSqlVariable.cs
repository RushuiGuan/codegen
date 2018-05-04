using Albatross.Database;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {

	[Cmdlet(VerbsCommon.New, "SqlVariable")]
	public class NewSqlVariable: Cmdlet {

		[Parameter(Position = 0, Mandatory = true)]
		public string Name { get; set; }

		[Parameter(Position = 1, Mandatory = true)]
		public Albatross.Database.SqlType Type { get; set; }


		protected override void ProcessRecord() {
			var variable = new Variable{
				Name = Name,
				Type = Type,
			};
			WriteObject(variable);
		}
	}
}