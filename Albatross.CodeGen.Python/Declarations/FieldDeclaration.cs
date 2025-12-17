using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class FieldDeclaration : CodeNode, IDeclaration {
		public FieldDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}

		public IdentifierNameExpression Identifier { get; }
		public ITypeExpression Type { get; init; } = Defined.Types.None;
		public IExpression? Initializer { get; init; }
		public override IEnumerable<ICodeNode> Children => new ICodeNode[] { Type, Identifier }.Concat(Initializer == null ? [] : [Initializer]);

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			if (!Type.Equals(Defined.Types.None)) {
				writer.Append(": ").Code(Type);
			}
			if (Initializer != null) {
				writer.Append(" = ").Code(Initializer);
			}
			return writer;
		}
	}
}