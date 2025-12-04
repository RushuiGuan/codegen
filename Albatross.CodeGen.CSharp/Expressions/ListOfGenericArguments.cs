using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfGenericArguments : ListOfSyntaxNodes<ITypeExpression> {
		public ListOfGenericArguments(params ITypeExpression[] nodes) : base(nodes) {
			LeftPadding = "<";
			RightPadding = ">";
		}
	}
}