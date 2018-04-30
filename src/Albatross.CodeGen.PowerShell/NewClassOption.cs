﻿using Albatross.CodeGen.Generation;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.New, "ClassOption")]
	public class NewClassOption : PSCmdlet {


		[Parameter]
		public string Name{ get; set; }

		[Parameter]
		[Alias("pre")]
		public string Prefix { get; set; }

		[Parameter]
		[Alias("post")]
		public string Postfix { get; set; }

		[Parameter]
		[Alias("b")]
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
			var meta = new ClassOption {
				Name = Name,
				Prefix = Prefix,
				Postfix = Postfix,
			};

			meta.Inheritance = Inheritance;
			if (!string.IsNullOrEmpty(AccessModifier)) { meta.AccessModifier = AccessModifier; }
			if (!string.IsNullOrEmpty(Namespace)) { meta.Namespace = Namespace; }
			if (Constructors != null) { meta.Constructors = Constructors; }
			if(Imports != null) { meta.Imports = Imports; }
			
			WriteObject(meta);
		}
	}
}