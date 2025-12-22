using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen {
	/// <summary>
	/// Represents a member access expression chain (e.g., obj.method().property.method2())
	/// </summary>
	public record class MemberAccessExpression : CodeNode, IExpression {
		private readonly bool lineBreak;

		/// <summary>
		/// Initializes a new instance of the MemberAccessExpression class
		/// </summary>
		/// <param name="expression">The base expression to access members from</param>
		/// <param name="lineBreak">Whether to format chained members with line breaks</param>
		/// <param name="members">The member expressions to access</param>
		/// <exception cref="ArgumentException">Thrown when no members are provided</exception>
		public MemberAccessExpression(IExpression expression, bool lineBreak, params IEnumerable<IExpression> members) {
			if (!members.Any()) {
				throw new ArgumentException("MemberAccessExpression must have at least one member.");
			}
			this.lineBreak = lineBreak;
			Expression = expression;
			Members = members;
		}

		/// <summary>
		/// Gets the base expression that members are accessed from
		/// </summary>
		public IExpression Expression { get; init; }
		
		/// <summary>
		/// Gets the collection of member expressions to access
		/// </summary>
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

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode>(Members) { Expression };
	}
}