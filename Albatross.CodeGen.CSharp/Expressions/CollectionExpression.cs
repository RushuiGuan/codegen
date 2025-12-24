using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class CollectionExpression : ListOfNodes<LiteralExpression>, IExpression {
		public CollectionExpression() {
			Prefix = "[";
			PostFix = "]";
		}
	}
}