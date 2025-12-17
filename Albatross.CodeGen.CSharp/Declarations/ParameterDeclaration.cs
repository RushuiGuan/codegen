using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class ParameterDeclaration : CodeNode, IDeclaration {
		public required ITypeExpression Type { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }
		public bool UseThisKeyword { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			foreach (var attrib in Attributes) {
				writer.Code(attrib);
			}
			if (UseThisKeyword) {
				writer.Code(Defined.Keywords.This);
			}
			return writer.Code(Type).Space().Code(Name);
		}

		public override IEnumerable<ICodeNode> Children
			=> Attributes.Cast<ICodeNode>().Concat([Type, Name]);
	}
}