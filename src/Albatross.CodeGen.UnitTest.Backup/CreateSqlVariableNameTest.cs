using Albatross.CodeGen.SqlServer;
using NUnit.Framework;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
    public class CreateSqlVariableNameTest{

		[TestCase("1Apple", ExpectedResult = "@1Apple")]
		[TestCase("Apple 1", ExpectedResult = "@apple1")]
		[TestCase("Apple1", ExpectedResult = "@apple1")]
		[TestCase("Apple&Sauce", ExpectedResult = "@apple_Sauce")]
		[TestCase(" Apple", ExpectedResult = "@apple")]
		[TestCase("Apple", ExpectedResult = "@apple")]
		[TestCase("apple", ExpectedResult ="@apple")]	
		[TestCase("_apple", ExpectedResult = "@_apple")]
		[TestCase("_apple_sauce", ExpectedResult = "@_apple_sauce")]
		[TestCase("apple sauce", ExpectedResult = "@appleSauce")]
		[TestCase("apple  sauce", ExpectedResult = "@appleSauce")]
		public string Test(string name) {
			return new CreateSqlVariableName().Get(name);
		}
    }
}
