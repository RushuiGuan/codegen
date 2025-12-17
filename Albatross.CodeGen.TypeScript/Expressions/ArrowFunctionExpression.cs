using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class ArrowFunctionExpression : CodeNode, IExpression, ICodeElement {
		public ListOfNodes<IIdentifierNameExpression> Arguments { get; init; } = new();
		public IExpression Body { get; init; } = new NoOpExpression();

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode> { Arguments, Body };

		public override TextWriter Generate(TextWriter writer) {
			if (Arguments.Count() == 1) {
				writer.Code(Arguments);
			} else {
				writer.OpenParenthesis().Code(Arguments).CloseParenthesis();
			}
			writer.Append(" => ").Code(Body);
			return writer;
		}
	}
}