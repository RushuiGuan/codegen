using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class BooleanTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => ["System.Boolean"];
		protected override ITypeExpression GetResult(ITypeSymbol symbol) => Defined.Types.Boolean;
	}
}