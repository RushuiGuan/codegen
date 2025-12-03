using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ParenthesizedExpression : SyntaxNode, IExpression {
		public required IExpression Expression { get; init; }
		public override IEnumerable<ISyntaxNode> Children => [Expression];
		public override TextWriter Generate(TextWriter writer)
			=> writer.Append('(').Code(Expression).Append(')');
	}
}
