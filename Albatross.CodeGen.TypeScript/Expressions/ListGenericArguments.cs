using System.Collections.Generic;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record ListOfGenericArguments : ListOfNodes<ITypeExpression>, IExpression {
		public ListOfGenericArguments() {
			LeftPadding = "<";
			RightPadding = ">";
		}
	}
}