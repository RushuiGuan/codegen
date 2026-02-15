using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest {
	public class TestCoreExpressions {
		private sealed record class LiteralNode(string Text) : CodeNode, IExpression {
			public override TextWriter Generate(TextWriter writer) {
				writer.Write(Text);
				return writer;
			}
		}

		[Fact]
		public void ParenthesizedExpression_ShouldWrapInnerExpression() {
			var expression = new ParenthesizedExpression(new LiteralNode("x"));

			var text = new StringWriter().Code(expression).ToString();

			text.Should().Be("(x)");
		}

		[Fact]
		public void InfixExpression_ShouldRespectParenthesisFlag() {
			var infix = new InfixExpression {
				Left = new LiteralNode("a"),
				Operator = new Operator("+"),
				Right = new LiteralNode("b"),
				UseParenthesis = true
			};

			var text = new StringWriter().Code(infix).ToString();

			text.Should().Be("(a + b)");
		}

		[Fact]
		public void MemberAccess_ShouldParenthesizeInfixBaseExpression() {
			var baseExpression = new InfixExpression {
				Left = new LiteralNode("a"),
				Operator = new Operator("+"),
				Right = new LiteralNode("b"),
			};
			var member = new MemberAccessExpression(baseExpression, false, [new LiteralNode("c")]);

			var text = new StringWriter().Code(member).ToString();

			text.Should().Be("(a + b).c");
		}

		[Fact]
		public void Chain_ShouldFlattenMemberAccessExpression() {
			var root = new MemberAccessExpression(new LiteralNode("root"), false, [new LiteralNode("first")]);

			var chain = root.Chain(false, new LiteralNode("second"));
			var text = new StringWriter().Code(chain).ToString();

			text.Should().Be("root.first.second");
		}

		[Fact]
		public void NoOpExpression_ShouldNotWriteAnything() {
			var noOp = new NoOpExpression();
			var writer = new StringWriter();

			noOp.Generate(writer);

			writer.ToString().Should().BeEmpty();
			noOp.GetDescendants().Should().BeEmpty();
		}
	}
}
