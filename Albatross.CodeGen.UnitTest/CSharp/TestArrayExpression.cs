using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestArrayExpression {

		[Fact]
		public void TestEmpty() {
			var expression = new ArrayExpression {
				Type = Defined.Types.String
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("new string[] {}", text);
		}

		[Fact]
		public void TestNormal() {
			var expression = new ArrayExpression {
				Type = Defined.Types.String,
				Items = {
					new StringLiteralExpression("One"),
					new StringLiteralExpression("Two"),
					new StringLiteralExpression("Three")
				}
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("new string[] { \"One\", \"Two\", \"Three\" }", text);
		}

		[Fact]
		public void TestMultiline() {
			var expression = new ArrayExpression {
				Type = Defined.Types.String,
				Items = {
					new StringLiteralExpression("One"),
					new StringLiteralExpression("Two"),
					new StringLiteralExpression("Three")
				}
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal(@"new string[] {
	""One"",
	""Two"",
	""Three""
}", text);
		}
	}
}
