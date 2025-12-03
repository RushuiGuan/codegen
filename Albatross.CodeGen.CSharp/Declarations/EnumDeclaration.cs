using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class EnumDeclaration : ISyntaxNode, IDeclaration{
		public AccessModifier ? AccessModifier { get; init; } = AccessModifier.Public;
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }
		public required IExpression[] Members { get; init; }
		
		public TextWriter Generate(TextWriter writer) {
			foreach (var attribute in AttributeExpressions) {
				writer.Code(attribute).WriteLine();
			}
			if(AccessModifier != null){
				writer.Append(AccessModifier.Name).Space();
			}
			writer.Append("enum ").Code(Name).Space();
			using (var scope = writer.BeginScope()) {
				foreach(var member in Members){
					scope.Writer.Code(member).WriteLine();
				}
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>(AttributeExpressions){
				Name
			};
			list.AddRange(Members);
			return list;
		}
	}
}