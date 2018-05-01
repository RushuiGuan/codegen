using Albatross.CodeGen.Core;
using System.Management.Automation;

namespace Albatross.PowerShell {
	public partial class Ioc {
		private Ioc() {
			new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(container);
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);
			container.Verify();
		}
	}
}
