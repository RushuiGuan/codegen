using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class DateTimeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.DateTime",
			"System.DateTimeOffset"
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.DateTime;
	}
}