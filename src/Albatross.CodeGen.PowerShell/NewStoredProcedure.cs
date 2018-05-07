using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.CodeGen.PowerShell.Transformation;
using Albatross.Database;
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

		[Parameter]
		public Albatross.Database.Database Database { get; set; }

		[Parameter(ValueFromPipeline = true)]
		[Alias("gs")]
		public object GenSource { get; set; }

		[Parameter]
		[Array2Branch]
		public Branch Branch { get; set; }

		[Parameter]
		public DatabasePermission[] Permissions { get; set; }

		[Parameter]
		public FilterOption Filter { get; set; }


		protected override void ProcessRecord() {
			Procedure procedure = new Procedure {
				Name = Name,
				Schema = Schema,
				Database = Database,
				Permissions = Permissions,
			};

			if (GenSource != null && Branch != null && Branch.Nodes != null) {
				if (GenSource is PSObject psObj) { GenSource = psObj.BaseObject; }
				StringBuilder sb = new StringBuilder();
				IRunCodeGenerator codeGen = Ioc.Get<IRunCodeGenerator>();
				Composite c = new Composite(typeof(object), typeof(SqlCodeGenOption)) {
					Branch = new  Branch(new Leaf("sql.procedure"), Branch),
				};
				var meta = c.GetMeta();
				codeGen.Run(meta, sb, GenSource, new SqlCodeGenOption());
				procedure.CreateScript = sb.ToString();
			}
		}
		SqlCodeGenOption Get(bool alterProcedure) {
			return new SqlCodeGenOption {
				Name = Name,
				Schema = Schema,
				AlterProcedure = alterProcedure,
			};
		}
	}
}

