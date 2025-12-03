using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NameSpaceExpression : SyntaxNode, ISourceExpression {
		public required MultiPartIdentifierNameExpression Name { get; init; }

		public override IEnumerable<ISyntaxNode> Children => [Name];

		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Name);
		}
	}
}