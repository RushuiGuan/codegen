using Albatross.CodeGen.Python.TypeConversions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestExtensions {
	[Fact]
	public void BeginPythonScope_ShouldCreateColonScopedBlock() {
		using var writer = new StringWriter();
		writer.Write("if True");
		using (var scope = writer.BeginPythonScope()) {
			scope.Writer.Write("pass");
		}

		var text = writer.ToString().Replace("\r", "").TrimEnd();

		text.Should().Be("if True:\n\tpass");
	}

	[Fact]
	public void BeginPythonLineBreak_ShouldWrapWithGivenDelimiters() {
		using var writer = new StringWriter();
		using (var scope = writer.BeginPythonLineBreak("{", "}")) {
			scope.Writer.Write("x: 1");
		}

		var text = writer.ToString().Replace("\r", "").TrimEnd();

		text.Should().Be("{\n\tx: 1\n}");
	}

	[Fact]
	public void AddTypeConverters_ShouldRegisterKnownConvertersWithoutDuplicates() {
		var services = new ServiceCollection();

		services.AddTypeConverters(typeof(ConvertType).Assembly);
		var once = services.Count(x => x.ServiceType == typeof(ITypeConverter));

		services.AddTypeConverters(typeof(ConvertType).Assembly);
		var twice = services.Count(x => x.ServiceType == typeof(ITypeConverter));

		twice.Should().Be(once);
		services.Should().Contain(x => x.ServiceType == typeof(ITypeConverter) && x.ImplementationType == typeof(StringTypeConverter));
		services.Should().Contain(x => x.ServiceType == typeof(ITypeConverter) && x.ImplementationType == typeof(NullableTypeConverter));
	}
}
