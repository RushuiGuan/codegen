namespace Albatross.CodeGen.Python.Expressions {
	public record class ListTypeExpression : GenericTypeExpression {
		public ListTypeExpression() : base(Defined.Identifiers.List) {
		}
	}
}