using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class JsonPropertyExpression : CodeNode, IExpression {
		public IdentifierNameExpression Identifier { get; }
		public IExpression Expression { get; }
		public override IEnumerable<ICodeNode> Children => [Identifier, Expression];

		public JsonPropertyExpression(string name, IExpression expression) {
			this.Identifier = new IdentifierNameExpression(name);
			this.Expression = expression;
		}

		public override TextWriter Generate(TextWriter writer) {
			if (Identifier.Equals(Expression)) {
				writer.Code(Identifier);
			} else {
				writer.Code(Identifier).Append(": ").Code(Expression);
			}
			return writer;
		}
	}
}