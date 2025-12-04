using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class EnumDeclaration : IDeclaration {
		public AccessModifierKeyword? AccessModifier { get; init; } = Defined.Keywords.Public;

		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<AttributeExpression> Attributes { get; init; } = [];
		public required IExpression[] Members { get; init; }

		public TextWriter Generate(TextWriter writer) {
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

		public IEnumerable<ISyntaxNode> GetDescendants()
			=> new List<ISyntaxNode>(Attributes) {
				Name
			}.Concat(Members);
	}
}