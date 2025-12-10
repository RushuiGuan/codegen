using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class GenericTypeConverter : ITypeConverter {
		public int Precedence => 100;

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			if (symbol is INamedTypeSymbol named && named.IsGenericType) {
				expression = new GenericTypeExpression(symbol.Name) {
					Arguments = new ListOfSyntaxNodes<ITypeExpression>(((symbol as INamedTypeSymbol)?.TypeArguments ?? []).Select(factory.Convert))
				};
				return true;
			} else {
				expression = null;
				return false;
			}
		}
	}
}