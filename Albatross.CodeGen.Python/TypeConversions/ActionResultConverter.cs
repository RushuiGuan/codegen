using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.SymbolProviders;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class ActionResultConverter : ITypeConverter {
		private readonly Compilation compilation;
		public int Precedence => 80;

		public ActionResultConverter(Compilation compilation) {
			this.compilation = compilation;
		}

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			if (compilation.IActionResultInterface().Is(symbol) || compilation.ActionResultClass().Is(symbol)) {
				expression = Defined.Types.Any;
				return true;
			} else if (compilation.TryGetActionResultGenericDefinition(out var definition) && symbol.TryGetGenericTypeArguments(definition, out var arguments)) {
				expression = factory.Convert(arguments[0]);
				return true;
			} else {
				expression = null;
				return false;
			}
		}
	}
}