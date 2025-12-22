using Albatross.CodeAnalysis;
using Albatross.CodeGen.SymbolProviders;
using Microsoft.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Albatross.CodeGen.WebClient.Models {
	public record class ParameterInfo {
		public ParameterInfo(Compilation compilation, IParameterSymbol symbol, IRouteSegment[] routeSegments) {
			this.Name = symbol.Name;
			this.Type = symbol.Type;
			if (symbol.TryGetAttribute(compilation.FromHeaderAttributeClass(), out _)) {
				// the header parameter should be setup during proxy registration
				this.Skip = true;
			} else if (symbol.TryGetAttribute(compilation.FromBodyAttributeClass(), out var attribute)) {
				this.WebType = ParameterType.FromBody;
			} else if (symbol.TryGetAttribute(compilation.FromRouteAttributeClass(), out attribute)) {
				this.WebType = ParameterType.FromRoute;
				var segment = FindSegment(Name, routeSegments);
				if (segment != null) {
					segment.ParameterInfo = this;
				}
			} else if (symbol.TryGetAttribute(compilation.FromQueryAttributeClass(), out attribute)) {
				this.WebType = ParameterType.FromQuery;
				if (attribute!.TryGetNamedArgument("Name", out var name)) {
					this.QueryKey = System.Convert.ToString(name.Value) ?? string.Empty;
				} else {
					this.QueryKey = this.Name;
				}
			} else {
				var segment = FindSegment(Name, routeSegments);
				if (segment != null) {
					this.WebType = ParameterType.FromRoute;
					segment.ParameterInfo = this;
				} else {
					this.WebType = ParameterType.FromQuery;
					this.QueryKey = this.Name;
				}
			}
		}

		RouteParameterSegment? FindSegment(string name, IRouteSegment[] segments) {
			foreach (var segment in segments) {
				if (segment is RouteParameterSegment parameterSegment
					&& string.Equals(parameterSegment.Text, name, System.StringComparison.InvariantCultureIgnoreCase)) {
					return parameterSegment;
				}
			}
			return null;
		}

		public bool Skip { get; }
		public string QueryKey { get; set; } = string.Empty;
		public string Name { get; set; }

		[JsonIgnore]
		public ITypeSymbol Type { get; set; }

		public string TypeText => Type.GetFullName();
		public ParameterType WebType { get; set; }
		// TODO: implement wildcard route parameter support
		// TODO: update RouteParameterSegment to indicate wildcard
		// public bool IsWildCardRoute { get; set; }
	}
}