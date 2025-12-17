using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class JsonElementConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Text.Json.JsonElement",
			"System.Text.Json.JsonDocument",
		];

		protected override ITypeExpression GetResult(ITypeSymbol symbol)
			=> Defined.Types.Any;
	}
}