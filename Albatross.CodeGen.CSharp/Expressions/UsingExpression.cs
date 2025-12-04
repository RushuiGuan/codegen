using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class UsingExpression : IExpression {
		public required IExpression Resource { get; init; }
		public IExpression Body { get; init; } = new NoOpExpression();

		public TextWriter Generate(TextWriter writer) {
			var scope = writer.Code(Defined.Keywords.Using).BeginScope();
			scope.Writer.Code(Body);
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [Resource, Body];
	}
}