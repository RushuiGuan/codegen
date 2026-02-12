using Albatross.CodeAnalysis;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.Collections;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Albatross.CodeGen.WebClient.Models {
	/// <summary>
	/// Represents metadata extracted from an ASP.NET Core controller class for code generation
	/// </summary>
	public record class ControllerInfo {
		public const string ControllerNamePlaceholder = "[controller]";
		public const string ControllerPostfix = "Controller";

		[JsonIgnore]
		public INamedTypeSymbol Controller { get; }
		public string ControllerName {
			get {
				if (this.Controller.Name.EndsWith(ControllerPostfix)) {
					return this.Controller.Name.Substring(0, this.Controller.Name.Length - ControllerPostfix.Length);
				} else {
					return this.Controller.Name;
				}
			}
		}
		public string Route { get; set; }
		public List<MethodInfo> Methods { get; } = new List<MethodInfo>();
		public bool IsObsolete { get; set; }
		public bool RequiresAuthentication { get; }


		public ControllerInfo(Compilation compilation, INamedTypeSymbol controller) {
			this.Controller = controller;
			this.Route = controller.GetRouteText();
			this.Route = this.Route.Replace(ControllerNamePlaceholder, this.ControllerName.ToLower());
			this.IsObsolete = controller.GetAttributes().Any(x => x.AttributeClass?.GetFullName() == My.ObsoleteAttribute_ClassName);
			this.RequiresAuthentication = controller.GetAttributes().Any(x => x.AttributeClass?.GetFullName() == My.AuthorizeAttribute_ClassName);

			foreach (var methodSymbol in controller.GetMembers().OfType<IMethodSymbol>()) {
				if (methodSymbol.GetAttributes().Any(x => x.AttributeClass?.BaseType?.GetFullName() == My.HttpMethodAttributeClassName)) {
					Methods.Add(new MethodInfo(compilation, methodSymbol));
				}
			}
		}

		public void ApplyMethodFilters(IEnumerable<Settings.SymbolFilter> filters) {
			var controllerName = this.Controller.GetFullName();
			this.Methods.RemoveAny(method => !filters.ShouldKeep($"{controllerName}.{method.Name}"));
		}
	}
}