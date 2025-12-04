using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class MultiTypeExpression : ITypeExpression {
		ListOfSyntaxNodes<ITypeExpression> types;

		public MultiTypeExpression(params IEnumerable<ITypeExpression> nodes) {
			types = new ListOfSyntaxNodes<ITypeExpression> {
				Separator = " | ",
				Nodes = nodes,
			};
		}
		public TextWriter Generate(TextWriter writer) => writer.Code(types);
		public IEnumerable<ISyntaxNode> GetDescendants() => types;
	}
}