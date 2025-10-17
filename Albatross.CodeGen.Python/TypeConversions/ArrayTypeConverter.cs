using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class ArrayTypeConverter : ITypeConverter {
		public int Precedence => 80;

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			ITypeExpression typeExpression;
			if (symbol.TryGetCollectionElementType(out var elementType)) {
				if(elementType!.GetFullName() == "System.Byte") {
					expression = Defined.Types.Bytes;
					return true;
				}
				typeExpression = factory.Convert(elementType!);
			} else if (symbol.IsCollection()) {
				typeExpression = Defined.Types.Any;
			} else {
				expression = null;
				return false;
			}
			expression = new ListTypeExpression {
				Arguments = new ListOfSyntaxNodes<ITypeExpression>(typeExpression)
			};
			return true;
		}
	}
}