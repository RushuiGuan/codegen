using SimpleInjector;
using System.Management.Automation;
using Albatross.Database;

namespace Albatross.CodeGen.PowerShell {

	[Cmdlet(VerbsCommon.Get, "StoredProcedure")]
	public class GetStoredProcedure : PSCmdlet {
		const string DefaultServer = "localhost";
		const string ByName = "ByName";
		const string ByObject = "ByObject";
		const string ByRefresh = "ByRefresh";

		[Parameter]
		[Alias("c")]
		public string Criteria { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = ByName)]
		[Alias("ds")]
		public string DataSource { get; set; }

		[Parameter(Mandatory = false, ParameterSetName = ByName)]
		[Alias("s")]
		public string Server { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = ByObject)]
		public Albatross.Database.Database Database { get; set; }

		[Parameter(Mandatory = true, ParameterSetName = ByRefresh)]
		[Alias("p")]
		public Procedure Procedure{ get; set; }

		protected override void BeginProcessing() {
			base.BeginProcessing();
			new AssemblyRediret().Register<Container>();
		}

		protected override void ProcessRecord() {
			Albatross.Database.Database db;

			if (base.ParameterSetName == ByName) {
				db = new Albatross.Database.Database {
					SSPI = true,
					DataSource = DefaultServer,
					InitialCatalog = DataSource,
				};
			} else if (base.ParameterSetName == ByObject) {
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
