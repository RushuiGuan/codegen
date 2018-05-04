using Albatross.CodeGen.PowerShell.Transformation;
using Albatross.Database;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "SqlParameter")]
	public class NewSqlParameter: Cmdlet {

		[Parameter(Position = 0, Mandatory = true)]
		public string Name { get; set; }

		[Parameter(Position = 1, Mandatory = true)]
		[String2SqlType]
		public Albatross.Database.SqlType Type { get; set; }


		protected override void ProcessRecord() {
			var parameter = new Parameter{
				Name = Name,
				Type = Type,
			};
			WriteObject(parameter);
		}
	}
}