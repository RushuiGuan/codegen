using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest {
	public class TestListOfNodes {
		[Theory]
		[InlineData(false)]
		[InlineData(true)]
		public void TestEmpty(bool multiline) {
			var nodes = new ListOfNodes<IExpression> {
				Prefix = "(",
				PostFix = ")",
				LeftPadding = "_",
				RightPadding = "_",
				Separator = ";",
				Multiline = multiline
			};
			nodes.Add(new NoOpExpression());
			var text = new StringWriter().Code(nodes).ToString();
			Assert.Equal("()", text);
		}
		[Fact]
		public void TestNoOp() {
			var nodes = new ListOfNodes<IExpression> {
				new NoOpExpression(),
			};
			var text = new StringWriter().Code(nodes).ToString();
			Assert.Equal("", text);
		}

		[Fact]
		public void TestSingleLine() {
			var nodes = new ListOfNodes<IExpression> {
				Prefix = "(",
				PostFix = ")",
				LeftPadding = "_",
				RightPadding = "_",
				Separator = ", ",
				Multiline = false
			};
			nodes.Add(new CodeGen.CSharp.Expressions.IntLiteralExpression(1));
			nodes.Add(new CodeGen.CSharp.Expressions.IntLiteralExpression(2));
			nodes.Add(new CodeGen.CSharp.Expressions.IntLiteralExpression(3));

			var text = new StringWriter().Code(nodes).ToString();
			Assert.Equal("(_1, 2, 3_)", text);
		}

		[Fact]
		public void TestMultiline() {
			var nodes = new ListOfNodes<IExpression> {
				Prefix = "(",
				PostFix = ")",
				LeftPadding = "_",
				RightPadding = "_",
				Separator = ", ",
				Multiline = true
			};
			nodes.Add(new CodeGen.CSharp.Expressions.IntLiteralExpression(1));
			nodes.Add(new CodeGen.CSharp.Expressions.IntLiteralExpression(2));
			nodes.Add(new CodeGen.CSharp.Expressions.IntLiteralExpression(3));

			var text = new StringWriter().Code(nodes).ToString();
			Assert.Equal(@"(
	_1, 
	2, 
	3_
)", text);
		}
	}
}
