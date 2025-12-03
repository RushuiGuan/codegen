using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Models {
	public record class ConvertClassSymbolToDtoClassModel : IConvertObject<INamedTypeSymbol, DtoClassInfo> {
		private readonly Compilation compilation;
		private readonly IJsonDerivedTypeIndex derivedTypeindex;

		public ConvertClassSymbolToDtoClassModel(Compilation compilation, IJsonDerivedTypeIndex derivedTypeindex) {
			this.compilation = compilation;
			this.derivedTypeindex = derivedTypeindex;
		}

		public DtoClassInfo Convert(INamedTypeSymbol from) {
			return new DtoClassInfo(compilation, from, derivedTypeindex);
		}
		object IConvertObject<INamedTypeSymbol>.Convert(INamedTypeSymbol from) => Convert(from);
	}
}