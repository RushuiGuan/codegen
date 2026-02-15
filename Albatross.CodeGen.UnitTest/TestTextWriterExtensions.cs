using FluentAssertions;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest {
	public class TestTextWriterExtensions {
		private sealed record class LiteralNode(string Text) : CodeNode, IExpression {
			public override TextWriter Generate(TextWriter writer) {
				writer.Write(Text);
				return writer;
			}
		}

		[Fact]
		public void CodeIfNotNull_ShouldSkipNullElements() {
			var writer = new StringWriter();

			writer.CodeIfNotNull(null);
			writer.CodeIfNotNull(new LiteralNode("x"));

			writer.ToString().Should().Be("x");
		}
	}
}
