using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class IntTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Int16",
			"System.Int32",
			"System.Int64",
			"System.UInt16",
			"System.UInt32",
			"System.UInt64",
			"System.Byte",
			"System.SByte",
			"System.IntPtr",
			"System.UIntPtr"
		];
		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Int;
	}
}