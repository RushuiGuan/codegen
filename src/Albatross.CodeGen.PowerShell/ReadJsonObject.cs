using System;
using System.IO;
using System.Management.Automation;
using Newtonsoft.Json;

namespace Albatross.CodeGen.PowerShell {

	[Cmdlet(VerbsCommunications.Read, "JsonObject")]
	public class ReadJsonObject : PSCmdlet {
		[Parameter(Position = 0, ValueFromPipeline = true, Mandatory =true)]
		[Alias("j")]
		public string Json{ get; set; }

		[Parameter(Position = 1, Mandatory = true)]
		[Alias("t")]
		public Type Type { get; set; }


		[Alias("a")]
        [Parameter]
		public SwitchParameter Array { get; set; }


		protected override void ProcessRecord() {
			var reader = new JsonTextReader(new StringReader(Json));

			Type type = this.Type;
			if (Array.ToBool()) {
				type = this.Type.MakeArrayType();
			}
			var serializer = new JsonSerializer();
			var obj = serializer.Deserialize(reader, type);
			if (Array.ToBool()) {
				foreach(var item in ((System.Array)obj)){
					WriteObject(item);
				}
			} else {
				WriteObject(obj);
			}
		}
	}
}
