using Albatross.CodeGen.Syntax;
using System.Collections.Generic;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record ListOfGenericArguments : ListOfSyntaxNodes<ITypeExpression>, IExpression {
		public ListOfGenericArguments(params IEnumerable<ITypeExpression> nodes) : base(nodes) {
			LeftPadding = "<";
			RightPadding = ">";
		}
	}
}