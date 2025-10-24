using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class DateConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.DateOnly",
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Date;
	}
}