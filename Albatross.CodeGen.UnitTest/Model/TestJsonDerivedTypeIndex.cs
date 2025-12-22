using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.UnitTest.Model {
	public class TestJsonDerivedTypeIndex {
		const string Code = @"
using System.Text.Json.Serialization;
[JsonDerivedType(typeof(MyClassA), typeDiscriminator : ""A"")]
[JsonDerivedType(typeof(MyClassB), ""B"")]
public interface IMyInterface
{
	string Type { get; }
}

[JsonDerivedType(typeof(MyClassC), typeDiscriminator:""C"")]
[JsonDerivedType(typeof(MyClassD), ""D"")]
public class MyBaseClass
{
	string Type { get; }
}

public class MyClassA : IMyInterface
{
	public string Type => ""A"";
}
public class MyClassB : IMyInterface
{
	public string Type => ""B"";
}
public class MyClassC : MyBaseClass {
}
public class MyClassD : MyBaseClass {
}
";

		[Theory]
		[InlineData("MyClassA", "IMyInterface", "A")]
		[InlineData("MyClassB", "IMyInterface", "B")]
		[InlineData("MyClassC", "MyBaseClass", "C")]
		[InlineData("MyClassD", "MyBaseClass", "D")]
		public async Task TestClassWithInterfaceBaseType(string className, string expectedBaseClass, string expectedDiscriminator) {

			var compilation = await Code.CreateNet8CompilationAsync();
			var index = new JsonDerivedTypeIndex(compilation);
			var symbol = compilation.GetRequiredSymbol(className);
			var result = index.GetDerivedTypeDiscriminators(symbol);
			Assert.Single(result);
			Assert.Equal(result[0].Item1, compilation.GetRequiredSymbol(expectedBaseClass));
			Assert.Equal(expectedDiscriminator, result[0].Item2);
		}
	}
}