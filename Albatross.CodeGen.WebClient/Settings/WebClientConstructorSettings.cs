namespace Albatross.CodeGen.WebClient.Settings {
	public record class WebClientConstructorSettings {
		public string? CompressionEncoding { get; set; }
		public bool Omit { get; init; }
		public string? CustomJsonSettings { get; init; }
	}
}