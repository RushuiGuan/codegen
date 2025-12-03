using Albatross.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.SymbolProviders {
	public static class Extensions {
		public static bool TryGetActionResultGenericDefinition(this Compilation compilation, [NotNullWhen(true)] out INamedTypeSymbol? symbol) {
			symbol = compilation.GetTypeByMetadataName("Microsoft.AspNetCore.Mvc.ActionResult`1");
			return symbol != null;
		}
		
		public static bool TryGetIActionResult(this Compilation compilation, [NotNullWhen(true)] out INamedTypeSymbol? symbol) {
			symbol = compilation.GetTypeByMetadataName("Microsoft.AspNetCore.Mvc.IActionResult");
			return symbol != null;
		}
		
		public static bool TryGetActionResult(this Compilation compilation, [NotNullWhen(true)] out INamedTypeSymbol? symbol) {
			symbol = compilation.GetTypeByMetadataName("Microsoft.AspNetCore.Mvc.ActionResult");
			return symbol != null;
		}

		public static INamedTypeSymbol FromBodyAttributeClass(this Compilation compilation)
			=> compilation.GetRequiredSymbol("Microsoft.AspNetCore.Mvc.FromBodyAttribute");

		public static INamedTypeSymbol FromQueryAttributeClass(this Compilation compilation)
			=> compilation.GetRequiredSymbol("Microsoft.AspNetCore.Mvc.FromQueryAttribute");

		public static INamedTypeSymbol FromRouteAttributeClass(this Compilation compilation)
			=> compilation.GetRequiredSymbol("Microsoft.AspNetCore.Mvc.FromRouteAttribute");

		public static INamedTypeSymbol FromHeaderAttributeClass(this Compilation compilation)
			=> compilation.GetRequiredSymbol("Microsoft.AspNetCore.Mvc.FromHeaderAttribute");
	}
}