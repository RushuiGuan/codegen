using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "ObjectType")]
	public class NewObjectType : PSCmdlet {

		[Parameter(Position = 0, Mandatory = true)]
		public string ClassName { get; set;}

		[Parameter(Position = 0, Mandatory = true)]
		public string AssemblyLocation { get; set; }


		protected override void ProcessRecord() {
			var meta = new ObjectType {
				ClassName = ClassName,
				AssemblyLocation = AssemblyLocation,
			};
			WriteObject(meta);
		}
	}
}