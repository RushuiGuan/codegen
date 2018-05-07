using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "SqlQueryOption")]
	public class NewSqlQueryOption : Cmdlet {

		[Parameter]
		public FilterOption Filter { get; set; }

		[Parameter]
		public string Name { get; set; }

		[Parameter]
		public string Schema{ get; set; }

		protected override void ProcessRecord() {
			var value = new SqlCodeGenOption {
				Filter = Filter,
				Name = Name,
				Schema = Schema,
			};
			WriteObject(value);
		}
	}
}