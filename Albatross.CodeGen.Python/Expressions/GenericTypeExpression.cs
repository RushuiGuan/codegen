using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class GenericTypeExpression : CodeNode, ITypeExpression {
		public GenericTypeExpression(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}
		public GenericTypeExpression(IIdentifierNameExpression identifier) {
			this.Identifier = identifier;
		}
		public IIdentifierNameExpression Identifier { get; }
		public required ListOfNodes<ITypeExpression> Arguments { get; init; }
		public override IEnumerable<ICodeNode> Children => [Identifier, Arguments];
		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Identifier).Append("[").Code(Arguments).Append("]");
		}
	}
}