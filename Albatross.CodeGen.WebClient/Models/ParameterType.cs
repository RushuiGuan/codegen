using System.Text.Json.Serialization;

namespace Albatross.CodeGen.WebClient.Models {
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum ParameterType {
		FromBody,
		FromQuery,
		FromRoute,
	}
}