using Albatross.CodeGen.Core;
using Albatross.CodeGen.PowerShell.Transformation;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "Leaf")]
	public class NewLeaf : Cmdlet {

		[Parameter(Mandatory = true, Position = 0)]
		public string Name { get; set; }

		[Parameter(Mandatory = false, Position = 1, ValueFromPipeline = true)]
		[PSObject2ObjectAttribute]
		public object Source { get; set; }

		[Parameter(Mandatory = false, Position = 2)]
		[PSObject2ObjectAttribute]
		public ICodeGeneratorOption Option { get; set; }

		protected override void ProcessRecord() {
			WriteObject(new Leaf(Name) { Source = Source, Option = Option,});
		}
	}
}
