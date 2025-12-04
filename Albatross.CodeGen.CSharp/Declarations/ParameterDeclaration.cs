using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class ParameterDeclaration : IDeclaration {
		public required ITypeExpression Type { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }

		public TextWriter Generate(TextWriter writer) {
			foreach (var attrib in Attributes) {
				writer.Code(attrib);
			}
			return writer.Code(Type).Space().Code(Name).Semicolon();
		}

		public IEnumerable<ISyntaxNode> GetDescendants()
			=> new List<ISyntaxNode>(Attributes).UnionAll([Type, Name]);
	}
}