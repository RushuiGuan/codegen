using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class TimeSpanConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.TimeSpan",
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.TimeDelta;
	}
}