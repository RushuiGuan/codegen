using Albatross.CodeGen.Generation;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Get, "CRUDProjectOption")]
    public class GetCRUDProjectOption:PSCmdlet
    {
		[Parameter(ValueFromPipeline =true, Position = 0, Mandatory =true)]
		public string Content { get; set; }

		protected override void ProcessRecord() {
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CRUDProjectOptions>(Content);
			WriteObject(result);
		}
	}
}
