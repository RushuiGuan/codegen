using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Syntax {
	public record class MemberAccessExpression : SyntaxNode, IExpression {
		private readonly bool lineBreak;

		public MemberAccessExpression(IExpression expression, bool lineBreak, params IEnumerable<IExpression> members) {
			if (!members.Any()) {
				throw new ArgumentException("MemberAccessExpression must have at least one member.");
			}
			this.lineBreak = lineBreak;
			Expression = expression;
			Members = members;
		}

		public IExpression Expression { get; init; }
		public IEnumerable<IExpression> Members { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Expression is InfixExpression ? new ParenthesizedExpression(Expression) : Expression);
			if (lineBreak) {
				// start a new indent scope after the first member
				writer.Append(".").Code(Members.First());
				using var scope = writer.BeginIndentScope();
				scope.Writer.WriteItems(Members.Skip(1), "\n", (w, member) => w.Append(".").Code(member is InfixExpression ? new ParenthesizedExpression(member) : member));
			} else {
				foreach (var member in Members) {
					writer.Append(".").Code(member is InfixExpression ? new ParenthesizedExpression(member) : member);
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode>(Members) { Expression };
	}
}