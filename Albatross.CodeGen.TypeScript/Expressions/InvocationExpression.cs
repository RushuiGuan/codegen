using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class InvocationExpression : SyntaxNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IIdentifierNameExpression Identifier { get; init; }
		public bool Terminate { get; init; }
		public ListOfSyntaxNodes<IExpression> ArgumentList { get; init; } = new ListOfSyntaxNodes<IExpression>();
		public override IEnumerable<ISyntaxNode> Children => [Identifier, ArgumentList];

		public override TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) {
				writer.Append("await ");
			}
			writer.Code(Identifier);
			writer.OpenParenthesis().Code(ArgumentList).CloseParenthesis();
			if (Terminate) {
				writer.Append(";");
			}
			return writer;
		}
	}
}