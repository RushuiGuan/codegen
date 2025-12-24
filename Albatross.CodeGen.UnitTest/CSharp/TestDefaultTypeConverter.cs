using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.CSharp.TypeConversions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestDefaultTypeConverter {
		[Fact]
		public async Task TestGenericInterfaceConversion() {
			var code = @"
public class MyClass {}
public interface IRepository<T> {}
public class SqlRepository<T> : IRepository<T> {}
";
			var compilation = await code.CreateNet8CompilationAsync();
			var symbol = compilation.GetRequiredSymbol("IRepository`1").Construct(compilation.GetRequiredSymbol("MyClass"));	
			var converter = new DefaultTypeConverter();
			var expression = converter.Convert(symbol);
			var text = new StringWriter().Code(expression).ToString();
			Assert.Equal("IRepository<MyClass>", text);
		}
	}
}