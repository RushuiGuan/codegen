using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Models {
	public record class ConvertClassSymbolToDtoClassModel : IConvertObject<INamedTypeSymbol, DtoClassInfo> {
		private readonly IJsonDerivedTypeIndex derivedTypeindex;

		public ConvertClassSymbolToDtoClassModel(IJsonDerivedTypeIndex derivedTypeindex) {
			this.derivedTypeindex = derivedTypeindex;
		}

		public DtoClassInfo Convert(INamedTypeSymbol from) {
			return new DtoClassInfo(from, derivedTypeindex);
		}
		object IConvertObject<INamedTypeSymbol>.Convert(INamedTypeSymbol from) => Convert(from);
	}
}