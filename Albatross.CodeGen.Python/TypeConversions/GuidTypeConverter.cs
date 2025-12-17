using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class GuidTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Guid"
		];

		protected override ITypeExpression GetResult(ITypeSymbol symbol)
			=> Defined.Types.UUID;
	}
}