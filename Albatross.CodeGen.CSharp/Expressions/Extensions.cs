using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public static class Extensions {
		public static IExpression Chain(this IExpression expression, params IEnumerable<IExpression> members)
			=> new MemberAccessExpression(expression, members);

		public static IExpression Terminate(this IExpression expression)
			=> new SinglelineExpression(expression);
	}
}