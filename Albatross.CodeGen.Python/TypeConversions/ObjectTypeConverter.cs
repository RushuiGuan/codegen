using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class ObjectTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Object",
			"System.Text.Json.JsonElement"
		];

		protected override ITypeExpression GetResult(ITypeSymbol symbol) => Defined.Types.Object(symbol.NullableAnnotation == NullableAnnotation.Annotated);
	}
}