using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	public class SetJsonObject<T> : BaseCmdlet<JsonFileRepository<T>> where T : class {

		[Parameter(Position = 0, Mandatory = true)]
		[Alias("p")]
		public FileInfo Path { get; set; }

		[Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
		[Alias("v")]
		public T Value { get; set; }

		[Parameter]
		public SwitchParameter Force { get; set; }

		protected override void ProcessRecord() {
			if (!Handle.IsExisting(Path.FullName) || Force || this.ShouldContinue("The file already exists, continue and overwrite?", "Warning")) {
				Handle.Save(Value, Path.FullName);
			}
		}
	}
}
