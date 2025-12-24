using System.Collections.Generic;

namespace Albatross.CodeGen.Python.Expressions {
	public record ListOfGenericArguments : ListOfNodes<ITypeExpression>, IExpression {
		public ListOfGenericArguments() {
			LeftPadding = "[";
			RightPadding = "]";
		}
	}
}