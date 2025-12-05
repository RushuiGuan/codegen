using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class MultiTypeExpression : SyntaxNode, ITypeExpression {
		ListOfSyntaxNodes<ITypeExpression> types;

		public MultiTypeExpression(params IEnumerable<ITypeExpression> nodes) {
			types = new ListOfSyntaxNodes<ITypeExpression> {
				Separator = " | ",
				Nodes = nodes,
			};
		}
		public override TextWriter Generate(TextWriter writer) => writer.Code(types);
		public override IEnumerable<ISyntaxNode> Children => types;
	}
}