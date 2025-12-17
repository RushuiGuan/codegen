using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public static class Extensions {
		public static IExpression EndOfStatement(this IExpression expression)
			=> new StatementExpression(expression);
	}
}