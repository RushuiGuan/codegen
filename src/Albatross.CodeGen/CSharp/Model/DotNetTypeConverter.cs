using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Model {
	public class DotNetTypeArrayConverter : JsonConverter<DotNetType[]> {
		public override DotNetType[] ReadJson(JsonReader reader, Type objectType, DotNetType[] existingValue, bool hasExistingValue, JsonSerializer serializer) {
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, DotNetType[] values, JsonSerializer serializer) {
			writer.WriteStartArray();
			if(values != null) {
				foreach(var item in values) {
					serializer.Serialize(writer, item);
				}
			}
			writer.WriteEndArray();
		}
	}
}
