using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class UsingExpression : CodeNode, ICodeBlock{
		public required IExpression Resource { get; init; }
		public CodeBlock Body { get; } = new CSharpCodeBlock();

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.Using).Code(new ParenthesizedExpression(Resource)).Code(Body);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [Resource, Body];
	}
}