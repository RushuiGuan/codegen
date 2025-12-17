namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TupleExpression : ListOfNodes<IExpression>, IExpression {
		public TupleExpression(params IExpression[] items) : base(items) {
			Prefix = "(";
			PostFix = ")";
			Separator = ", ";
		}
	}
}