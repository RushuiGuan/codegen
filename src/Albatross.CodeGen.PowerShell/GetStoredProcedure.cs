using SimpleInjector;
using System.Management.Automation;
using Albatross.Database;

namespace Albatross.CodeGen.PowerShell {

	[Cmdlet(VerbsCommon.Get, "StoredProcedure")]
	public class GetStoredProcedure : PSCmdlet {
		const string DefaultServer = "localhost";

		//Criteria, DataSource, Server
		const string Set1 = "Set1";
		//Criteria, Database object
		const string Set2 = "Set2";
		//Procedure object
		const string Set3 = "Set3";

		[Parameter]
		[Alias("c")]
		public string Criteria { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = Set1)]
		[Alias("ds")]
		public string DataSource { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = Set1)]
		[Alias("s")]
		public string Server { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = Set2)]
		public Albatross.Database.Database Database { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = Set3, ValueFromPipeline =true)]
		[Alias("p")]
		public Procedure Procedure{ get; set; }

		protected override void BeginProcessing() {
			base.BeginProcessing();
			new AssemblyRediret().Register<Container>();
		}

		protected override void ProcessRecord() {
			Albatross.Database.Database db;

			if (base.ParameterSetName == Set1) {
				db = new Albatross.Database.Database {
					SSPI = true,
					DataSource = DefaultServer,
					InitialCatalog = DataSource,
				};
			} else if (base.ParameterSetName == Set2) {
				db = Database;
			} else {
				db = Procedure.Database;
				Criteria = $"{Procedure.Schema}.{Procedure.Name}";
			}

			var items = Ioc.Get<IListProcedure>().Get(db, Criteria);
			foreach (var item in items) {
				WriteObject(item);
			}
		}
	}
}
