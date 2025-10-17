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
	public class DateConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.DateOnly",
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Date;
	}
	public class TimeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.TimeOnly",
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.Time;
	}
	public class TimeSpanConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.TimeSpan",
		];

		protected override ITypeExpression GetResult(ITypeSymbol _) => Defined.Types.TimeDelta;
	}	
}