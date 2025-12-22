using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// Represents an infix expression with a left operand, operator, and right operand (e.g., a + b)
	/// </summary>
	public record class InfixExpression : CodeNode, IExpression {
		/// <summary>
		/// Gets or sets whether the expression should be wrapped in parentheses when rendered
		/// </summary>
		public bool UseParenthesis { get; init; }
		
		/// <summary>
		/// Gets or sets the operator for this infix expression
		/// </summary>
		public required IOperator Operator { get; init; }
		
		/// <summary>
		/// Gets or sets the left operand of the expression
		/// </summary>
		public required IExpression Left { get; init; }
		
		/// <summary>
		/// Gets or sets the right operand of the expression
		/// </summary>
		public required IExpression Right { get; init; }

		/// <summary>
		/// Generates the code representation of the infix expression
		/// </summary>
		/// <param name="writer">The TextWriter to write the generated code to</param>
		/// <returns>The TextWriter for method chaining</returns>
		public override TextWriter Generate(TextWriter writer) {
			if (UseParenthesis) { writer.OpenParenthesis(); }
			writer.Code(Left).Code(Operator).Code(Right);
			if (UseParenthesis) { writer.CloseParenthesis(); }
			return writer;
		}

		/// <summary>
		/// Gets the child nodes of this infix expression (left and right operands)
		/// </summary>
		public override IEnumerable<ICodeNode> Children => [Left, Right];
	}
}