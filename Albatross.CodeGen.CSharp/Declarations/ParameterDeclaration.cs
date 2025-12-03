using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class ParameterDeclaration: IDeclaration, ISyntaxNode {
		public required ITypeExpression Type { get; init; }
		public IEnumerable<AttributeExpression> AttributeExpressions { get; init; } = [];
		public required IdentifierNameExpression Name { get; init; }
		public TextWriter Generate(TextWriter writer) {
			return writer.Code(Type).Space().Code(Name).Semicolon();
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [Type, Name];
	}
}