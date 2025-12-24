using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfGenericArguments : ListOfNodes<ITypeExpression>, IExpression {
		public ListOfGenericArguments() {
			LeftPadding = "<";
			RightPadding = ">";
		}
	}
}