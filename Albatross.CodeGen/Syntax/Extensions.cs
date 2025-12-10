using System.Collections.Generic;

namespace Albatross.CodeGen.Syntax {
	public static class Extensions {
		public static IExpression Chain(this IExpression expression, bool lineBreak, params IEnumerable<IExpression> members)
			=> new MemberAccessExpression(expression, lineBreak, members);
	}
}