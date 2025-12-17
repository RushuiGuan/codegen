using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfGenericArguments : ListOfNodes<ITypeExpression>, IExpression {
		public ListOfGenericArguments(params IEnumerable<ITypeExpression> nodes) : base(nodes) {
			LeftPadding = "<";
			RightPadding = ">";
		}
	}
}