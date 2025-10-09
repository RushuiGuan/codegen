using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class SetPropertyDeclaration : SyntaxNode, IDeclaration, ICodeElement {
		public SetPropertyDeclaration(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}

		public IdentifierNameExpression Identifier { get; }
		public bool Optional { get; init; }
		public ITypeExpression[] Types { get; init; } = [];
		public override IEnumerable<ISyntaxNode> Children => new List<ISyntaxNode> { Identifier, Type };

		public IEnumerable<IModifier> Modifiers => throw new System.NotImplementedException();

		public override TextWriter Generate(TextWriter writer) {
			writer.Write(new DecoratorExpression {
				Identifier = new IdentifierNameExpression("property")
			});
			writer.Code(Identifier);
			if (Optional) {
				writer.Append(" ?");
			};
			writer.Append(": ").Code(Type);
			return writer;
		}
	}
}