namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TupleExpression : ListOfNodes<IExpression>, IExpression {
		public TupleExpression() {
			Prefix = "(";
			PostFix = ")";
			Separator = ", ";
		}
	}
}