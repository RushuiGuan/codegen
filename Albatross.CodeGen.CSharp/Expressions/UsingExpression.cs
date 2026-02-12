using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class UsingExpression : CodeNode, ICodeBlock{
		public required IExpression Resource { get; init; }
		public CodeBlock Body { get; } = new CSharpCodeBlock();

		public override TextWriter Generate(TextWriter writer) {
			if (Body.Any()) {
				writer.Code(Defined.Keywords.Using).Code(new ParenthesizedExpression(Resource)).Code(Body);
			} else {
				writer.Code(Defined.Keywords.Using).Code(Resource).Code(new EndOfStatement());
			}
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [Resource, Body];
	}
}