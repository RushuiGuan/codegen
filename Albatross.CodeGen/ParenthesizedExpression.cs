using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// Represents an expression wrapped in parentheses for grouping or precedence control
	/// </summary>
	public record class ParenthesizedExpression : CodeNode, IExpression {
		/// <summary>
		/// Initializes a new instance of the ParenthesizedExpression class
		/// </summary>
		/// <param name="expression">The expression to wrap in parentheses</param>
		public ParenthesizedExpression(IExpression expression) {
			this.Expression = expression;
		}

		/// <summary>
		/// Gets the expression that is wrapped in parentheses
		/// </summary>
		public IExpression Expression { get; }
		
		/// <summary>
		/// Generates the parenthesized expression code
		/// </summary>
		/// <param name="writer">The TextWriter to write the generated code to</param>
		/// <returns>The TextWriter for method chaining</returns>
		public override TextWriter Generate(TextWriter writer)
			=> writer.Append('(').Code(Expression).Append(')');
		/// <summary>
		/// Gets the child nodes of this parenthesized expression (the wrapped expression)
		/// </summary>
		public override IEnumerable<ICodeNode> Children => [Expression];
	}
}