using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class InfixExpression : SyntaxNode, IExpression {
		public bool UseParenthesis { get; init; }
		public required IOperator Operator { get; init; }
		public required IExpression Left { get; init; }
		public required IExpression Right { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			if (UseParenthesis) { writer.OpenParenthesis(); }
			writer.Code(Left).Code(Operator).Code(Right);
			if (UseParenthesis) { writer.CloseParenthesis(); }
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [Left, Right];
	}
}