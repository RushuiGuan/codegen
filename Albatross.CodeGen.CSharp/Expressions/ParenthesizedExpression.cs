using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ParenthesizedExpression : IExpression {
		public ParenthesizedExpression(IExpression expression) {
			this.Expression = expression;
		}

		public IExpression Expression { get; }
		public  TextWriter Generate(TextWriter writer)
			=> writer.Append('(').Code(Expression).Append(')');
		public IEnumerable<ISyntaxNode> GetDescendants() => [Expression];
	}
}