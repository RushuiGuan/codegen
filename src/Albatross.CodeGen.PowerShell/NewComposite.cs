using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.New, "Composite")]
    public class NewComposite: Cmdlet   {
		[Parameter(Mandatory = true, Position = 0)]
		[Alias("st")]
		public Type SourceType { get; set; }

		[Parameter(Mandatory = true, Position = 1)]
		[Alias("ot")]
		public Type OptionType { get; set; }

		[Parameter(Mandatory = true, Position = 2, ValueFromPipeline =true)]
		[Alias("b")]
		public Branch Branch { get; set; }

		[Parameter(Position = 3)]
		[Alias("n")]
		public string Name { get; set; }

		[Parameter(Position = 4)]
		[Alias("t")]
		public string Target { get; set; }

		[Parameter(Position = 5)]
		[Alias("c")]
		public string Category{ get; set; }

		[Parameter(Position = 6)]
		[Alias("d")]
		public string Description{ get; set; }
		

		protected override void ProcessRecord() {
			Composite c = new Composite(SourceType, OptionType) {
				Branch= Branch,
				Name = Name,
				Target = Target,
				Category = Category,
				Description = Description,
			};
			WriteObject(c);
		}
	}
}
