using SimpleInjector;
using Albatross.CodeGen.Core;
using System;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Get, "CodeGenerator")]
	public class GetCodeGenerator: PSCmdlet{
		[Parameter(Position = 0, ValueFromPipeline = true)]
		public string Name { get; set; }

		protected override void BeginProcessing() {
			base.BeginProcessing();
			new AssemblyRediret().Register<Container>();
		}

		protected override void ProcessRecord() {
			ICodeGeneratorFactory factory = Ioc.Get<ICodeGeneratorFactory>();

			if (string.IsNullOrEmpty(Name)) {
				foreach (var item in factory.Registrations) {
					WriteObject(item);
				}
			} else {
				foreach (var item in factory.Registrations) {
					if (string.Equals(item.Name, Name, StringComparison.InvariantCultureIgnoreCase)) {
						WriteObject(item);
					}
				}
			}
		}
	}
}
