using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	public class GetJsonObject<T> : BaseCmdlet<JsonFileRepository<T>> where T : class {
		public GetJsonObject() {
		}

		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
		[Alias("p")]
		public System.IO.FileInfo Path { get; set; }

		protected override void ProcessRecord() {
			T t = Handle.Get(Path.FullName);
			WriteObject(t);
		}
	}
}
