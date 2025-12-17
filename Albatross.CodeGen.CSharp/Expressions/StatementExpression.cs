using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class StatementExpression : CodeNode, IExpression {
		public IExpression Expression { get; init; }

		public StatementExpression() : this(new NoOpExpression()) { }

		public StatementExpression(IExpression expression) {
			this.Expression = expression;
		}


		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Expression).Semicolon();

		public override IEnumerable<ICodeNode> Children => [Expression];
	}
}