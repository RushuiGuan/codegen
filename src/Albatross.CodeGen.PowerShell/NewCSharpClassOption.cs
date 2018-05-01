using Albatross.CodeGen.Generation;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "CSharpClassOption")]
	public class NewCSharpClassOption : PSCmdlet {


		[Parameter]
		public string Name{ get; set; }

		[Parameter]
		[Alias("pre")]
		public string Prefix { get; set; }

		[Parameter]
		[Alias("post")]
		public string Postfix { get; set; }

		[Parameter]
		[Alias("h")]
		public string[] Inheritance { get; set; }

		[Parameter]
		[Alias("i")]
		public string[] Imports{ get; set; }

		[Parameter]
		[Alias("a")]
		public string AccessModifier { get; set; }

		[Parameter]
		[Alias("n")]
		public string Namespace { get; set; }

		[Parameter]
		[Alias("c")]
		public string[] Constructors{ get; set; }

		protected override void ProcessRecord() {
			var meta = new CSharpClassOption {
				Name = Name,
				Prefix = Prefix,
				Postfix = Postfix,
				Inheritance = Inheritance ?? new string[0],
				Namespace = Namespace,
				Constructors = Constructors ?? new string[0],
				Imports = Imports??new string[0],
				AccessModifier = AccessModifier ?? "public",
			};
			WriteObject(meta);
		}
	}
}