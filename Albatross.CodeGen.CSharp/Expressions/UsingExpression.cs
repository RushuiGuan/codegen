using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class UsingExpression : CodeNode, IExpression {
		public required IExpression Resource { get; init; }
		public IExpression Body { get; init; } = new NoOpExpression();

		public override TextWriter Generate(TextWriter writer) {
			using var scope = writer.Code(Defined.Keywords.Using).Code(new ParenthesizedExpression(Resource)).BeginScope();
			scope.Writer.Code(Body);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children => [Resource, Body];
	}
}