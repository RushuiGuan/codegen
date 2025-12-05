using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class MemberAccessExpression : SyntaxNode, IExpression {
		public MemberAccessExpression(IExpression expression, params IEnumerable<IExpression> members) {
			Expression = expression;
			Members = members;
		}

		public IExpression Expression { get; init; }
		public IEnumerable<IExpression> Members { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			if (Expression is InfixExpression) {
				writer.Code(new ParenthesizedExpression(Expression));
			} else {
				writer.Code(Expression);
			}
			foreach (var member in Members) {
				writer.Append(".");
				if (member is InfixExpression) {
					writer.Code(new ParenthesizedExpression(member));
				} else {
					writer.Code(member);
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => new ISyntaxNode[] { Expression }.Concat(Members);
	}
}