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
		public Variable[] Variables { get; set; }

		[Parameter]
		public Parameter[] Parameters { get; set; }

		[Parameter]
		public Hashtable Expressions { get; set; }


		protected override void ProcessRecord() {
			var value = new SqlCodeGenOption {
				ExcludePrimaryKey = ExcludePrimaryKey.ToBool(),
				Filter = Filter,
				Name = Name,
				Schema = Schema,
				Variables = Variables,
				Parameters = Parameters,
			};
			if (Expressions != null) {
				foreach (var key in Expressions.Keys) {
					value.Expressions.Add(Convert.ToString(key), Convert.ToString(Expressions[key]));
				}
			}
			WriteObject(value);
		}
	}
}