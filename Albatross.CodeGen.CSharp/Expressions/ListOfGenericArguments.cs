using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfGenericArguments : ListOfSyntaxNodes<ITypeExpression>, IExpression {
		public ListOfGenericArguments(params IEnumerable<ITypeExpression> nodes) :base(nodes) {
			LeftPadding = "<";
			RightPadding = ">";
		}
	}
}