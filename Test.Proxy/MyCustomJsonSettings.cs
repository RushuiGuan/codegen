using System;
using System.Text.Json;

namespace Test.Proxy {
	public class MyCustomJsonSettings {
		readonly static Lazy<JsonSerializerOptions> lazy = new Lazy<JsonSerializerOptions>(
			() => new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true,
			});

		public static JsonSerializerOptions Instance => lazy.Value;
	}
}