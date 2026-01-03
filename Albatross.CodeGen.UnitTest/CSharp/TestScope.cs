using Albatross.Text;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestScope {
		[Fact]
		public void TestNormal() {
			var writer = new StringWriter();
			using (var scope = writer.Append("test").BeginScope("{", "}")) {
				scope.Writer.Append("test");
			}
			Assert.Equal("test{\r\n\ttest\n}", writer.ToString());
		}
	}
}
