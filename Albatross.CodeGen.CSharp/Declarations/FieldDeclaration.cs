using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class FieldDeclaration : CodeNode, IDeclaration {
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Private;
		public required ITypeExpression Type { get; init; }
		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public bool IsConst { get; init; }
		public IExpression? Initializer { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			foreach (var attribute in Attributes) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if (IsConst) { writer.Code(Defined.Keywords.Const); }
			writer.Code(Type).Space().Code(Name);
			if (Initializer != null) {
				writer.Code(Defined.Operators.Assignment).Code(Initializer);
			}
			writer.Semicolon();
			return writer;
		}

		public override IEnumerable<ICodeNode> Children {
			get => new List<ICodeNode>(Attributes) {
				Type, Name
			}.AddIfNotNull(Initializer);
		}
	}
}