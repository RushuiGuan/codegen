using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ArrayLiteralExpression : ListOfSyntaxNodes<LiteralExpression>, IExpression {
		public ArrayLiteralExpression(params IEnumerable<LiteralExpression> items) : base(items) {
			Prefix = "[";
			PostFix = "]";
		}
	}
}