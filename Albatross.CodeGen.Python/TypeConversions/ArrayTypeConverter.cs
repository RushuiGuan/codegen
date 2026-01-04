using Albatross.CodeAnalysis;
using Albatross.CodeGen.Python.Expressions;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class ArrayTypeConverter : ITypeConverter {
		private readonly Compilation compilation;
		public int Precedence => 80;
		public ArrayTypeConverter(ICompilationFactory factory) {
			this.compilation = factory.Get();
		}

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			ITypeExpression typeExpression;
			if (symbol.TryGetCollectionElementType(compilation, out var elementType)) {
				if (SymbolEqualityComparer.Default.Equals(elementType, compilation.Byte())) {
					expression = Defined.Types.Bytes;
					return true;
				}
				typeExpression = factory.Convert(elementType!);
			} else if (symbol.IsCollection(compilation)) {
				typeExpression = Defined.Types.Any;
			} else {
				expression = null;
				return false;
			}
			expression = new ListTypeExpression {
				Arguments = new ListOfNodes<ITypeExpression> {
					typeExpression
				}
			};
			return true;
		}
	}
}