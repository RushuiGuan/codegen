using Albatross.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.SymbolProviders {

	public static class Extensions {
		public static INamedTypeSymbol ActionResultGenericDefinition(this Compilation compilation)
			=> compilation.GetRequiredSymbol("Microsoft.AspNetCore.Mvc.ActionResult`1");
		
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