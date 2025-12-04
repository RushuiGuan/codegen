using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class VariableDeclaration : SyntaxNode, IExpression {
		public required IdentifierNameExpression Identifier { get; init; }
		public ITypeExpression Type { get; init; } = Defined.Types.Var;
		public IExpression? Assignment { get; init; }

		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode> { Identifier, Type }.AddIfNotNull(Assignment);

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Type).Space().Code(Identifier);
			if (Assignment != null) {
				writer.Append(" = ").Code(Assignment);
			}
			writer.Semicolon();
			return writer;
		}
	}
}