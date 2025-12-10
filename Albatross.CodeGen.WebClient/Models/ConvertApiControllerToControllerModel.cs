using Albatross.CodeGen.WebClient.Settings;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Models {
	public class ConvertApiControllerToControllerModel : IConvertObject<INamedTypeSymbol, ControllerInfo> {
		private readonly Compilation compilation;

		public ConvertApiControllerToControllerModel(Compilation compilation) {
			this.compilation = compilation;
		}
		public ControllerInfo Convert(INamedTypeSymbol controllerSymbol) {
			return new ControllerInfo(compilation, controllerSymbol);
		}
		object IConvertObject<INamedTypeSymbol>.Convert(INamedTypeSymbol from) => Convert(from);
	}
}