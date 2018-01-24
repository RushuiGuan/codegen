using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "SqlQueryOption")]
	public class NewSqlQueryOption : Cmdlet {

		public SwitchParameter ExcludePrimaryKey { get; set; }


		protected override void ProcessRecord() {
			var value = new SqlQueryOption {
				ExcludePrimaryKey = ExcludePrimaryKey.ToBool(),
			};
			WriteObject(value);
		}
	}
}