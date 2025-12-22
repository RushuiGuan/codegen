namespace Albatross.CodeGen.CSharp.Expressions {
	/// <summary>
	/// Represents a qualified identifier name expression that includes namespace source information
	/// </summary>
	public record class QualifiedIdentifierNameExpression : IdentifierNameExpression {
		/// <summary>
		/// Gets the namespace source for this qualified identifier
		/// </summary>
		public NamespaceExpression Source { get; }

		/// <summary>
		/// Initializes a new instance of the QualifiedIdentifierNameExpression class
		/// </summary>
		/// <param name="name">The identifier name</param>
		/// <param name="source">The namespace source for qualification</param>
		public QualifiedIdentifierNameExpression(string name, NamespaceExpression source) : base(name) {
			this.Source = source;
		}
	}
}