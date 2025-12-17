using Albatross.CodeGen.Python.Expressions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class ObjectTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Object",
		];

		protected override ITypeExpression GetResult(ITypeSymbol symbol) =>
			symbol.NullableAnnotation == NullableAnnotation.Annotated
				? new MultiTypeExpression(Defined.Types.None, Defined.Types.Object)
				: Defined.Types.Object;
	}
}