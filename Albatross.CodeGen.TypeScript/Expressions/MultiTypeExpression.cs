using System.Collections.Generic;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class MultiTypeExpression : ListOfNodes<ITypeExpression>, ITypeExpression {
		public MultiTypeExpression() {
			this.Separator = "|";
		}
	}
}