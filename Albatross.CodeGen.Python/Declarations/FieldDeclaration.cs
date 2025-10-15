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
		public ITypeExpression[] Types { get; init; } = [];
		public override IEnumerable<ISyntaxNode> Children => Types.Cast<ISyntaxNode>().Union([Identifier]).ToArray();

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier).Append(": ").WriteItems(Types, "|", (w, x) => w.Code(x));
			return writer;
		}
	}
}