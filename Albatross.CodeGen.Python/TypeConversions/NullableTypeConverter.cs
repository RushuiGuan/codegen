using Albatross.CodeAnalysis;
using Albatross.CodeGen.Python.Expressions;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class NullableTypeConverter : ITypeConverter {
		private readonly Compilation compilation;
		public int Precedence => 80;
		public NullableTypeConverter(ICompilationFactory factory) {
			this.compilation = factory.Get();
		}

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			ITypeExpression? typeExpression = null;
			if (symbol.TryGetNullableValueType(compilation, out var valueType)) {
				typeExpression = factory.Convert(valueType);
			} else if (symbol.IsNullableReferenceType()) {
				// strip nullable annotation
				typeExpression = factory.Convert(symbol.WithNullableAnnotation(NullableAnnotation.None));
			}
			if (typeExpression != null) {
				expression = new MultiTypeExpression { typeExpression, Defined.Types.None };
				return true;
			} else {
				expression = null;
				return false;
			}
		}
	}
}