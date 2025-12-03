using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Models {
	public record class ConvertEnumSymbolToDtoEnumModel : IConvertObject<INamedTypeSymbol, EnumInfo> {
		private readonly Compilation compilation;

		public ConvertEnumSymbolToDtoEnumModel(Compilation compilation) {
			this.compilation = compilation;
		}

		public EnumInfo Convert(INamedTypeSymbol from) {
			return new EnumInfo(compilation, from);
		}

		object IConvertObject<INamedTypeSymbol>.Convert(INamedTypeSymbol from) => Convert(from);
	}
}