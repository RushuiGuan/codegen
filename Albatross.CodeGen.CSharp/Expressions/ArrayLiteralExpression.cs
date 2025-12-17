using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ArrayLiteralExpression : ListOfNodes<LiteralExpression>, IExpression {
		public ArrayLiteralExpression(params IEnumerable<LiteralExpression> items) : base(items) {
			Prefix = "[";
			PostFix = "]";
		}
	}
}