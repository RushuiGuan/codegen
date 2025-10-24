namespace Albatross.CodeGen.WebClient.Models {
	public record class RouteTextSegment : IRouteSegment {
		public RouteTextSegment(string text) {
			this.Text = text;
		}
		public string Text { get; }
	}
}