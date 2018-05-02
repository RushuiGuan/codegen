using Albatross.CodeGen.Core;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "Leaf")]
	public class NewLeaf : Cmdlet {

		[Parameter(Mandatory = true, Position = 0)]
		public string Name { get; set; }

		[Parameter(Mandatory = false, Position = 1, ValueFromPipeline = true)]
		public object Source { get; set; }

		protected override void ProcessRecord() {
			WriteObject(new Leaf(Name) { Source = Source, });
		}
	}
}
