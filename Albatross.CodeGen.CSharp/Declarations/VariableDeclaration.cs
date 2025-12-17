using Albatross.CodeGen.CSharp.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Declarations {
	public record class VariableDeclaration : CodeNode, IExpression {
		public required IdentifierNameExpression Identifier { get; init; }
		public ITypeExpression Type { get; init; } = Defined.Types.Var;

		public override IEnumerable<ICodeNode> Children => [Identifier, Type];

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Type).Space().Code(Identifier);
	}
}