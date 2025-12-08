using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record ListOfGenericArguments : SyntaxNode, IExpression {
		readonly ListOfSyntaxNodes<ITypeExpression> list;

		public ListOfGenericArguments(params IEnumerable<ITypeExpression> nodes) {
			list = new ListOfSyntaxNodes<ITypeExpression>(nodes) {
				LeftPadding = "<",
				RightPadding = ">"
			};
		}

		public override TextWriter Generate(TextWriter writer) => writer.Code(list);
		public override IEnumerable<ISyntaxNode> Children => list;
	}
}