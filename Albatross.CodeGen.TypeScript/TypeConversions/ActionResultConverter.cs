using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.SymbolProviders;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.TypeScript.TypeConversions {
	public class ActionResultConverter : ITypeConverter {
		private readonly Compilation compilation;
		public int Precedence => 80;
		
		public ActionResultConverter(Compilation compilation) {
			this.compilation = compilation;
		}

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			var name = symbol.GetFullName();
			if (name == "Microsoft.AspNetCore.Mvc.IActionResult" || name == "Microsoft.AspNetCore.Mvc.ActionResult") {
				expression = Defined.Types.Any();
				return true;
			} else if (symbol.TryGetGenericTypeArguments(compilation.ActionResultGenericDefinition(), out var arguments)) {
				expression = factory.Convert(arguments[0]);
				return true;
			} else {
				expression = null;
				return false;
			}
		}
	}
}