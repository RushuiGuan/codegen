using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.Expressions {
	public record ListGenericArguments : ListOfSyntaxNodes<ITypeExpression>, IExpression {
		public ListGenericArguments(params IEnumerable<ITypeExpression> nodes) : base(nodes) {
			LeftPadding = "[";
			RightPadding = "]";
		}
	}
}