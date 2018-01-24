using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsDiagnostic.Test, "Pipeline")]
    public class TestPipeline:Cmdlet
    {
		[Parameter(Mandatory = true, ValueFromPipeline =true)]
		public object Input { get; set; }

		protected override void ProcessRecord() {
			PSObject obj = Input as PSObject;
			WriteObject(obj?.BaseObject?.GetType());
		}
	}
}
