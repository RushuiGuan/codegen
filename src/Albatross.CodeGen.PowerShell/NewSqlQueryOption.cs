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
		public SwitchParameter ExcludePrimaryKey { get; set; }

		[Parameter]
		public FilterOption Filter { get; set; }

		[Parameter]
		public string Name { get; set; }

		[Parameter]
		public string Schema{ get; set; }

		[Parameter]
		public SwitchParameter AlterProcedure { get; set; }


		protected override void ProcessRecord() {
			var value = new SqlCodeGenOption {
				ExcludePrimaryKey = ExcludePrimaryKey.ToBool(),
				Filter = Filter,
				Name = Name,
				Schema = Schema,
				AlterProcedure = AlterProcedure.ToBool(),
			};
			WriteObject(value);
		}
	}
}