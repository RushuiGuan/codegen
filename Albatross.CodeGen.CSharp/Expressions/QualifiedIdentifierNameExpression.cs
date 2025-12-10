namespace Albatross.CodeGen.CSharp.Expressions {
	public record class QualifiedIdentifierNameExpression : IdentifierNameExpression {
		public NamespaceExpression Source { get; }

		public QualifiedIdentifierNameExpression(string name, NamespaceExpression source) : base(name) {
			this.Source = source;
		}
	}
}