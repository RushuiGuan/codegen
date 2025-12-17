using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class NumericTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Single",
			"System.Double",
		];
		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Float;
	}
}