using System;
using System.Text.Json;

namespace Test.WithInterface.Proxy {
	public class MyCustomJsonSettings {
		readonly static Lazy<JsonSerializerOptions> lazy = new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions {
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			PropertyNameCaseInsensitive = true,
		});
		public static JsonSerializerOptions Options => lazy.Value;
	}
}