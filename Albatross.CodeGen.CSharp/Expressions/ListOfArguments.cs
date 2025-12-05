using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record ListOfArguments : SyntaxNode, IExpression {
		ListOfSyntaxNodes<IExpression> list;
		public ListOfArguments(params IEnumerable<IExpression> nodes) {
			list = new ListOfSyntaxNodes<IExpression> {
				Nodes = nodes,
				Prefix = "(",
				PostFix = ")"
			};
		}

		public override TextWriter Generate(TextWriter writer) => writer.Code(list);
		public override IEnumerable<ISyntaxNode> Children => list;
	}
}