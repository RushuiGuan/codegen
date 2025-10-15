using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class DateTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.DateOnly",
			"System.DateTime",
			"System.TimeSpan",
			"System.TimeOnly",
			"System.DateTimeOffset"
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Date();
	}
}