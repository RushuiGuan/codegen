using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class FieldDeclaration : SyntaxNode, IDeclaration {
		public FieldDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}

		public IdentifierNameExpression Identifier { get; }
		public ITypeExpression Type { get; init; } = Defined.Types.None;
		public override IEnumerable<ISyntaxNode> Children => [Type, Identifier];

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier).Append(": ").Code(Type);
			return writer;
		}
	}
}