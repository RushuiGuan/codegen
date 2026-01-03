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
			if(expression is InfixExpression) {
				expression = new ParenthesizedExpression(expression);
			}
			var list = new List<IExpression>();
			list.Add(expression);
			list.AddRange(members);
			Members = list;
		}

		/// <summary>
		/// Gets the collection of member expressions to access
		/// </summary>
		public IEnumerable<IExpression> Members { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			var delimiter = lineBreak ? "\n\t." : ".";
			writer.WriteItems(Members, delimiter, (w, x) => w.Code(x));
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => Members;
	}
}