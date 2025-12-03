using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class FieldDeclaration : IDeclaration, ISyntaxNode{
		public required ITypeExpression Type { get; init; }
		public required IdentifierNameExpression Name { get; init; }
		public AccessModifier? AccessModifier { get; init; } = AccessModifier.Private;
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		
		public TextWriter Generate(TextWriter writer) {
			foreach (var attribute in AttributeExpressions) {
				writer.Code(attribute).WriteLine();
			}
			if(AccessModifier != null){
				writer.Append(AccessModifier.Name).Space();
			}
			writer.Code(Type).Space().Code(Name).Semicolon();
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [Type, Name];
	}
}