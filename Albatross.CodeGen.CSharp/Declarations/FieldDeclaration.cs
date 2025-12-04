using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class FieldDeclaration : IDeclaration, ISyntaxNode {
		public required ITypeExpression Type { get; set; }
		public required IdentifierNameExpression Name { get; set; }
		public AccessModifier? AccessModifier { get; set; } = AccessModifier.Private;
		public IEnumerable<AttributeExpression> AttributeExpressions { get; set; } = [];
		public bool IsConst { get; set; }
		public IExpression? Initializer { get; set; }

		public TextWriter Generate(TextWriter writer) {
			foreach (var attribute in AttributeExpressions) {
				writer.Code(attribute).WriteLine();
			}
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			if (IsConst) { writer.Append(" const "); }
			writer.Code(Type).Space().Code(Name);
			if (Initializer != null) {
				writer.Append(" = ").Code(Initializer);
			}
			writer.Semicolon();
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>{
				Type, Name 
			};
			list.AddRange(AttributeExpressions);
			return list;
		}
	}
}