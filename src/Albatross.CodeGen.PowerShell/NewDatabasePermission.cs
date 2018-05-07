using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "DatabasePermission")]
	public class NewDatabasePermission : PSCmdlet {
		[Parameter(Position = 0)]
		[Alias("s")]
		public string State { get; set; }

		[Parameter(Position = 1)]
		[Alias("perm")]
		public string Permission { get; set; }

		[Alias("prin")]
		[Parameter(Position = 2)]
		public string Principal { get; set; }



		protected override void ProcessRecord() {
			base.ProcessRecord();
			WriteObject(new DatabasePermission {
				State = State,
				Permission = Permission,
				Principal = Principal,
			});
		}
	}
}
