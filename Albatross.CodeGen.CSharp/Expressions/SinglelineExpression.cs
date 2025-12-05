using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class SinglelineExpression : SyntaxNode, IExpression {
		private readonly IExpression expression;

		public SinglelineExpression(IExpression expression) {
			this.expression = expression;
		}

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(expression).Semicolon();

		public override IEnumerable<ISyntaxNode> Children => [expression];
	}
}