using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class TimeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.TimeOnly",
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Time;
	}
}