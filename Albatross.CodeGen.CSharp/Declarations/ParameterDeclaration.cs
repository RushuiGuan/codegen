using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class ParameterDeclaration : IDeclaration {
		public required ITypeExpression Type { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }
		public bool UseThisKeyword { get; init; }

		public TextWriter Generate(TextWriter writer) {
			foreach (var attrib in Attributes) {
				writer.Code(attrib);
			}
			if (UseThisKeyword) {
				writer.Code(Defined.Keywords.This);
			}
			return writer.Code(Type).Space().Code(Name);
		}

		public IEnumerable<ISyntaxNode> GetDescendants()
			=> Attributes.Cast<ISyntaxNode>().Concat([Type, Name]);
	}
}