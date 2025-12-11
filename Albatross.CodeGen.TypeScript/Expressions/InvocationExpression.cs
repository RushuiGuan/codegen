using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class InvocationExpression : SyntaxNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IIdentifierNameExpression CallableExpression { get; init; }
		public bool Terminate { get; init; }
		public ListOfSyntaxNodes<IExpression> Arguments { get; init; } = new ListOfSyntaxNodes<IExpression>();
		public override IEnumerable<ISyntaxNode> Children => [CallableExpression, Arguments];

		public override TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) {
				writer.Append("await ");
			}
			writer.Code(CallableExpression);
			writer.OpenParenthesis().Code(Arguments).CloseParenthesis();
			if (Terminate) {
				writer.Append(";");
			}
			return writer;
		}
	}
}