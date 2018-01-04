using System.IO;
using System.Management.Automation;
using System.Reflection;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Get, "DefaultLocation")]
    public class GetDefaultLocation: BaseCmdlet<IGetDefaultRepoFolder>
    {
		protected override void ProcessRecord() {
			WriteObject(Handle.Get());
			WriteObject(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
		}
	}
}
