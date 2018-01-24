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

		[Parameter(Mandatory =true, Position =0)]
		[Alias("n")]
		public string Name { get; set; }

		[Parameter(Mandatory = true, Position = 1)]
		[Alias("t")]
		public string Target { get; set; }

		[Parameter(Mandatory = true, Position = 2)]
		[Alias("st")]
		public Type SourceType { get; set; }

		[Parameter(Mandatory = true, Position = 3)]
		[Alias("g")]
		public string[] Generators { get; set; }

		[Parameter(Position = 4)]
		[Alias("c")]
		public string Category{ get; set; }

		[Parameter(Position = 5)]
		[Alias("d")]
		public string Description{ get; set; }
		
		[Parameter(Position = 6)]
		[Alias("s")]
		public string Seperator{ get; set; }

		protected override void ProcessRecord() {
			WriteObject(new Composite {
				Name = Name,
				Category = Category,
				Description = Description,
				SourceType	= SourceType,
				Target = Target,
				Generators = Generators,
				Seperator = Seperator,
			});
		}
	}
}
