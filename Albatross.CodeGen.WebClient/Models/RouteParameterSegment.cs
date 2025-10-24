namespace Albatross.CodeGen.WebClient.Models {
	public record class RouteParameterSegment : IRouteSegment {
		public RouteParameterSegment(string text) {
			this.Text = text;
		}

		public ParameterInfo? ParameterInfo { get; set; }
		public ParameterInfo RequiredParameterInfo => ParameterInfo ?? throw new System.InvalidOperationException("ParameterInfo is not set");
		public string Text { get; }
	}
}