using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class DecimalTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Decimal",
		];
		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Decimal;
	}
}