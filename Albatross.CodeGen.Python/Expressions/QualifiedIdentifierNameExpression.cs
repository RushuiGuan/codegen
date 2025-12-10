using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.Python.Expressions {
	public record class QualifiedIdentifierNameExpression : IdentifierNameExpression {
		public QualifiedIdentifierNameExpression(string name, ISourceExpression source) : base(name) {
			this.Source = source;
		}

		public ISourceExpression Source { get; }
	}
}