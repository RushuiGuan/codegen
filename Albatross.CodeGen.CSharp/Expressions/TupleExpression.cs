using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TupleExpression : ListOfSyntaxNodes<IExpression> {
		public TupleExpression(params IExpression[] items) : base(items) {
			Prefix = "(";
			PostFix = ")";
			Separator = ", ";
		}
	}
}