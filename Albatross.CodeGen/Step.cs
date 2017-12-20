using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	[JsonConverter(typeof(StepConverter))]
	public class Step {
		public Type SourceType { get; set; }
		public object Source { get; set; }
		public string Generator { get; set; }
		public string Options { get; set; }
	}

	public class StepConverter : JsonConverter {
		public override bool CanConvert(Type objectType) {
			return objectType == typeof(Step);
		}
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			Step step = new Step();
			reader.Read();
			step.SourceType = Type.GetType(reader.ReadAsString());
			reader.Read();
			reader.Read();
			step.Source = serializer.Deserialize(reader, step.SourceType);
			reader.Read();
			step.Generator = reader.ReadAsString();
			reader.Read();
			step.Options = reader.ReadAsString();
			while (reader.Read()) { }
			return step;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			throw new NotSupportedException();
		}
	}
}
