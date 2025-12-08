using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.Syntax;
using Albatross.Testing;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestAnonymousMethodExpression {
		[Fact]
		public void TestEmpty() {
			var expression = new AnonymousMethodExpression();
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("() => { }", text);
		}
		[Fact]
		public void TestSingleTypeParameter() {
			var expression = new AnonymousMethodExpression {
				Parameters = [
					new ParameterDeclaration {
						Type = new TypeExpression("int"),
						Name = new IdentifierNameExpression("x")
					}
				]
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("(int x) => { }", text);
		}
		[Fact]
		public void TestSingleVarParameter() {
			var expression = new AnonymousMethodExpression {
				Parameters = [
					new ParameterDeclaration {
						Type = Defined.Types.Var,
						Name = new IdentifierNameExpression("x")
					}
				]
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("x => { }", text);
		}
		[Fact]
		public void TestMultipleTypedParameter() {
			var expression = new AnonymousMethodExpression {
				Parameters = [
					new ParameterDeclaration {
						Type = Defined.Types.Int,
						Name = new IdentifierNameExpression("x")
					},
					new ParameterDeclaration {
						Type = Defined.Types.Int,
						Name = new IdentifierNameExpression("y")
					}
				]
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("(int x, int y) => { }", text);
		}
		[Fact]
		public void TestMultipleVarParameter() {
			var expression = new AnonymousMethodExpression {
				Parameters = [
					new ParameterDeclaration {
						Type = Defined.Types.Var,
						Name = new IdentifierNameExpression("x")
					},
					new ParameterDeclaration {
						Type = Defined.Types.Var,
						Name = new IdentifierNameExpression("y")
					}
				]
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("(x, y) => { }", text);
		}
		[Fact]
		public void TestSingleExpression() {
			var expression = new AnonymousMethodExpression {
				Body = [
					new StringLiteralExpression("hello")
				]
			};
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("() => \"hello\"", text);
		}
		[Fact]
		public void TestMultilineBody() {
			var expression = new AnonymousMethodExpression {
				Body = [
					new AssignmentExpression{
						Left = new VariableDeclaration{
							Type = Defined.Types.Int,
							Identifier = new IdentifierNameExpression("x"),
						},
						Expression = new IntLiteralExpression(10)
					}.EndOfStatement(),
					new ReturnExpression{
						Expression = new IdentifierNameExpression("x")
					}.EndOfStatement(),
					new NewLineExpression(),
				]
			};
			var text = new StringWriter().Code(expression).ToString();
			"() => {\tint x = 10;\n\treturn x;\n}".EqualsIgnoringLineEndings(text);
		}
	}
}