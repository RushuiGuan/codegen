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
		[Parameter(ValueFromPipeline = true)]
		[Alias("gs")]
		public object GenSource { get; set; }

		[Parameter]
		public Albatross.Database.Database Database { get; set; }

		[Parameter]
		[Array2Branch]
		public Branch Branch { get; set; }

		[Parameter]
		public DatabasePermission[] Permissions { get; set; }


		const string PSName_ByObject = "ByObject";
		const string PSName_ByFields = "ByFields";

		[Parameter(Mandatory = true, ParameterSetName = PSName_ByFields)]
		public string Name { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = PSName_ByFields)]
		public string Schema{ get; set; }

		[Parameter(ParameterSetName = PSName_ByFields)]
		public FilterOption Filter { get; set; }

		[Parameter(ParameterSetName = PSName_ByObject)]
		public SqlCodeGenOption Option { get; set; }

		protected override void ProcessRecord() {
			if (this.ParameterSetName == PSName_ByFields) {
				Option = new SqlCodeGenOption {
					Name = Name,
					Schema = Schema,
					Filter = Filter,
				};
			}

			Procedure procedure = new Procedure {
				Name = Option.Name,
				Schema = Option.Schema,
				Database = Database,
				Permissions = Permissions,
			};

			if (GenSource != null && Branch != null) {
				if (GenSource is PSObject psObj) { GenSource = psObj.BaseObject; }
				StringBuilder sb = new StringBuilder();
				IRunCodeGenerator codeGen = Ioc.Get<IRunCodeGenerator>();
				Composite c = new Composite(typeof(object), typeof(SqlCodeGenOption)) {
					Branch = new  Branch(new Leaf("sql.procedure.create"), Branch),
				};
				var meta = c.GetMeta();
				codeGen.Run(meta, sb, GenSource, Option);
				procedure.CreateScript = sb.ToString();

				sb.Length = 0;
				c = new Composite(typeof(object), typeof(SqlCodeGenOption)) {
					Branch = new Branch(new Leaf("sql.procedure.alter"), Branch),
				};
				meta = c.GetMeta();
				codeGen.Run(meta, sb, GenSource, Option);
				procedure.AlterScript = sb.ToString();

				sb.Length = 0;
				Ioc.Get<Albatross.CodeGen.Core.ICodeGeneratorFactory>().Create("sql.permission").Generate(sb, procedure, null);
				procedure.PermissionScript = sb.ToString();
			}

			WriteObject(procedure);
		}
	}
}

