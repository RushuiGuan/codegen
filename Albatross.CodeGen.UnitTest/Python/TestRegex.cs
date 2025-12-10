using Albatross.CodeGen.Python.Expressions;
using Xunit;

namespace Albatross.CodeGen.UnitTest.Python {
	public class TestRegex {

		[Theory]
		[InlineData(".xyz", true)]
		[InlineData("..xyz", true)]
		[InlineData("xyz", true)]
		[InlineData("x.y.z", true)]
		[InlineData("z.", false)]
		public void TestModuleRegex(string text, bool expected) {
			var match = ModuleSourceExpression.ModuleSource.Match(text);
			Assert.Equal(expected, match.Success);
		}
	}
}