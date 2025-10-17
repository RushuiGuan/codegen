using Albatross.CodeGen.Syntax;
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
		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Float;
	}
	public class NumericTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Single",
			"System.Double",
		];
		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Float;
	}
	public class DecimalTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.Decimal",
		];
		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Decimal;
	}
}