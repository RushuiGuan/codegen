using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public static class Extensions {
		public static IExpression Chain(this IExpression expression, params IEnumerable<IExpression> members)
			=> new MemberAccessExpression(expression, members);
	}
}