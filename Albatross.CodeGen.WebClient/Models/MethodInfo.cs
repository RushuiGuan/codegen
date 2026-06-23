using Albatross.CodeAnalysis;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Albatross.CodeGen.WebClient.Models {
	public record class MethodInfo {
		private readonly Compilation compilation;

		public MethodInfo(Compilation compilation, IMethodSymbol symbol, string controllerRoute = "") {
			this.compilation = compilation;
			this.Name = symbol.Name;
			this.ReturnType = GetReturnType(symbol.ReturnType);
			var controllerRouteSegments = controllerRoute.GetRouteSegments().ToArray();
			var routeSegments = symbol.GetRouteText().GetRouteSegments().ToArray();
			// match parameters against both the controller-level and method-level route templates so
			// that controller route parameters are classified as FromRoute and bound to their segment
			var allRouteSegments = controllerRouteSegments.Concat(routeSegments).ToArray();
			this.HttpMethod = GetHttpMethod(symbol);
			foreach (var parameter in symbol.Parameters) {
				var paramInfo = new ParameterInfo(compilation, parameter, allRouteSegments);
				if (paramInfo.TypeText == "System.Threading.CancellationToken") {
					this.CanCancel = true;
				} else {
					this.Parameters.Add(paramInfo);
				}
			}
			this.ControllerRouteSegments = controllerRouteSegments;
			this.RouteSegments = routeSegments;
			this.IsObsolete = symbol.GetAttributes().Any(x => x.AttributeClass?.GetFullName() == My.ObsoleteAttribute_ClassName);
			this.RequiresAuthentication = symbol.GetAttributes().Any(x => x.AttributeClass?.GetFullName() == My.AuthorizeAttribute_ClassName);
		}
		public string HttpMethod { get; set; }
		public string Name { get; set; }
		public bool CanCancel { get; set; }
		public bool RequiresAuthentication { get; set; }

		[JsonIgnore]
		public ITypeSymbol ReturnType { get; set; }
		public string ReturnTypeText => ReturnType.GetFullName();
		/// <summary>
		/// Route segments declared at the controller level (via its [Route] template).  These carry the
		/// route parameters that are shared by every method of the controller.
		/// </summary>
		public IEnumerable<IRouteSegment> ControllerRouteSegments { get; } = System.Array.Empty<IRouteSegment>();
		public bool HasControllerRouteParameter => ControllerRouteSegments.Any(x => x is RouteParameterSegment);
		public IEnumerable<IRouteSegment> RouteSegments { get; }
		public List<ParameterInfo> Parameters { get; } = new List<ParameterInfo>();
		public bool HasQueryStringParameter => Parameters.Any(x => x.WebType == ParameterType.FromQuery);
		public bool IsObsolete { get; set; }

		ITypeSymbol GetReturnType(ITypeSymbol type) {
			if (type is INamedTypeSymbol named && named.IsGenericType) {
				var genericTypeFullName = named.OriginalDefinition.GetFullName();
				switch (genericTypeFullName) {
					case My.GenericTaskClassName:
					case My.GenericActionResultClassName:
						return GetReturnType(named.TypeArguments[0]);
					case My.AsyncEnumerableClassName:
						return compilation.GetSpecialType(SpecialType.System_Collections_Generic_IEnumerable_T)
							.Construct(named.TypeArguments[0]);
					default:
						return named;
				}
			} else {
				var typeFullName = type.GetFullName();
				switch (typeFullName) {
					case My.TaskClassName:
					case My.ActionResultClassName:
					case My.ActionResultInterfaceName:
						return compilation.GetSpecialType(SpecialType.System_Void);
					default:
						return type;
				}
			}
		}
		string GetHttpMethod(IMethodSymbol symbol) {
			foreach (var attribute in symbol.GetAttributes()) {
				switch (attribute.AttributeClass?.GetFullName()) {
					case My.HttpGetAttributeClassName:
						return "Get";
					case My.HttpPostAttributeClassName:
						return "Post";
					case My.HttpPutAttributeClassName:
						return "Put";
					case My.HttpDeleteAttributeClassName:
						return "Delete";
					case My.HttpPatchAttributeClassName:
						return "Patch";
				}
			}
			return string.Empty;
		}
	}
}
