using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class EnumDeclaration : SyntaxNode, IDeclaration {
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;

		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public required IExpression[] Members { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			foreach (var attribute in Attributes) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			writer.Append("enum ").Code(Name).Space();
			using (var scope = writer.BeginScope()) {
				foreach (var member in Members) {
					scope.Writer.Code(member).WriteLine();
				}
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get => new List<ISyntaxNode>(Attributes) {
				Name
			}.Concat(Members);
		}
	}
}