using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "SqlQueryOption")]
	public class NewSqlQueryOption : Cmdlet {

		[Parameter]
		public SwitchParameter ExcludePrimaryKey { get; set; }


		[Parameter]
		public FilterOption Filter { get; set; }


		protected override void ProcessRecord() {
			var value = new SqlCodeGenOption {
				ExcludePrimaryKey = ExcludePrimaryKey.ToBool(),
				Filter = Filter,
			};
			WriteObject(value);
		}
	}
}