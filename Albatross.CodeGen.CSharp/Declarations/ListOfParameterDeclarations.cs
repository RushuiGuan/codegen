using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ListOfParameterDeclarations : IExpression {
		ListOfSyntaxNodes<ParameterDeclaration> list;
		public ListOfParameterDeclarations(params IEnumerable<ParameterDeclaration> items) {
			list = new ListOfSyntaxNodes<ParameterDeclaration> {
				Nodes = items,
				Prefix = "(",
				PostFix = ")"
			};
		}

		public TextWriter Generate(TextWriter writer) => writer.Code(list);
		public IEnumerable<ISyntaxNode> GetDescendants() => list;
	}
}