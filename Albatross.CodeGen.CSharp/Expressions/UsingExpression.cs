using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class UsingExpression : IExpression {
		public required IExpression Resource { get; init; }
		public IExpression Body { get; init; } = new NoOpExpression();

		public TextWriter Generate(TextWriter writer) {
			using var scope = writer.Code(Defined.Keywords.Using).Code(new ParenthesizedExpression(Resource)).BeginScope();
			scope.Writer.Code(Body);
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [Resource, Body];
	}
}