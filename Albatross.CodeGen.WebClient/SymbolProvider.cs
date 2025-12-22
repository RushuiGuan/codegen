using Albatross.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient {
	public static class SymbolProvider {
		public static INamedTypeSymbol JsonDerivedTypeAttribute(this Compilation compilation) => compilation.GetRequiredSymbol("System.Text.Json.Serialization.JsonDerivedTypeAttribute");
	}
}