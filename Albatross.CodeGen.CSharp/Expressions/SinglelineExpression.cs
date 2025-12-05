using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public class SinglelineExpression : IExpression {
		private readonly IExpression expression;

		public SinglelineExpression(IExpression expression) {
			this.expression = expression;
		}

		public TextWriter Generate(TextWriter writer)
			=> writer.Code(expression).Semicolon();

		public IEnumerable<ISyntaxNode> GetDescendants() => [expression];
	}
}