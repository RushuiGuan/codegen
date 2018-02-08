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
			Type type = typeof(Composite<,>);
			type = type.MakeGenericType(SourceType, OptionType);
			object obj = Activator.CreateInstance(type);
			type.GetProperty(nameof(Branch)).SetValue(obj, Branch);

			type.GetProperty(nameof(Name)).SetValue(obj, Name);
			type.GetProperty(nameof(Target)).SetValue(obj, Target);
			type.GetProperty(nameof(Category)).SetValue(obj, Category);
			type.GetProperty(nameof(Description)).SetValue(obj, Description);

			WriteObject(obj);
		}
	}
}
