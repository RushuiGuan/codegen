using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.TypeScript.TypeConversions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.UnitTest.TypeScript {
	public class TestTypeConverters {
		ILogger<ConvertType> MockedLogger => new Moq.Mock<ILogger<ConvertType>>().Object;

		[Theory]
		[InlineData(@"class Example { public System.Collections.Generic.IEnumerable<string> P1 { get; } }")]
		[InlineData(@"class Example { public string[] P1 { get; } }")]
		[InlineData(@"class Example { public System.Collections.Generic.List<string> P1 { get; } }")]
		[InlineData(@"class Example { public System.Collections.Generic.ICollection<string> P1 { get; } }")]
		public async Task TestStringArray(string code) {
			var compilation = await code.CreateNet8CompilationAsync();

			var symbol = compilation.GetRequiredSymbol("Example");
			var p1Symbol = symbol.GetMembers().OfType<IPropertySymbol>().Where(x => x.Name == "P1").First();
			var factory = new ConvertType([new StringTypeConverter()], MockedLogger);
			var converter = new ArrayTypeConverter(compilation);
			Assert.True(converter.TryConvert(p1Symbol.Type, factory, out var result));
			Assert.IsType<ArrayTypeExpression>(result);
			var expression = result as ArrayTypeExpression;
			Assert.Equal(Defined.Types.String(), expression?.Type);
		}

		[Theory]
		[InlineData(@"class Example { public byte[] P1 { get; } }")]
		public async Task TestByteArray(string code) {
			var compilation = await code.CreateNet8CompilationAsync();

			var symbol = compilation.GetRequiredSymbol("Example");
			var p1Symbol = symbol.GetMembers().OfType<IPropertySymbol>().Where(x => x.Name == "P1").First();
			var factory = new ConvertType([new StringTypeConverter(), new ArrayTypeConverter(compilation)], MockedLogger);
			var result = factory.Convert(p1Symbol.Type);
			Assert.Equal(Defined.Types.String(), result);
		}

		[Theory]
		[InlineData(@"class Example { public string? P1 { get; } }")]
		[InlineData(@"class Example { public System.String? P1 { get; } }")]
		public async Task TestNullableString(string code) {
			var compilation = await code.CreateNet8CompilationAsync();

			var symbol = compilation.GetRequiredSymbol("Example");
			var p1Symbol = symbol.GetMembers().OfType<IPropertySymbol>().Where(x => x.Name == "P1").First();
			var factory = new ConvertType([new StringTypeConverter(), new ArrayTypeConverter(compilation)], MockedLogger);
			var result = factory.Convert(p1Symbol.Type);
			Assert.Equal(Defined.Types.String(true), result);
		}
	}
}