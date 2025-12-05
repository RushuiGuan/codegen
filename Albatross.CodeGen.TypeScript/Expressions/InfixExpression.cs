using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class InfixExpression : SyntaxNode, IExpression {
		public InfixExpression(string @operator) {
			Operator = @operator;
		}

		public string Operator { get; }
		public bool UseParenthesis { get; init; }
		public required IExpression Left { get; init; }
		public required IExpression Right { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			if (UseParenthesis) { writer.OpenParenthesis(); }
			writer.Code(Left).Append(" ").Append(Operator).Append(" ").Code(Right);
			if (UseParenthesis) { writer.CloseParenthesis(); }
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [Left, Right];
	}
}