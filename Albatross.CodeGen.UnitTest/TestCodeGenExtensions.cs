using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Albatross.CodeGen.UnitTest {
	public class TestCodeGenExtensions {
		private sealed class IntToStringConverter : IConvertObject<int, string> {
			public string Convert(int from) => from.ToString();
			object IConvertObject<int>.Convert(int from) => Convert(from);
		}

		private sealed class NotAConverter { }

		[Fact]
		public void TryAddConverter_ShouldRegisterTypedAndUntypedInterfaces() {
			var services = new ServiceCollection();

			var added = services.TryAddConverter(typeof(IntToStringConverter));

			added.Should().BeTrue();
			services.Should().Contain(x => x.ServiceType == typeof(IntToStringConverter));
			services.Should().Contain(x => x.ServiceType == typeof(IConvertObject<int>));
			services.Should().Contain(x => x.ServiceType == typeof(IConvertObject<int, string>));
		}

		[Fact]
		public void TryAddConverter_ShouldReturnFalseForNonConverterType() {
			var services = new ServiceCollection();

			var added = services.TryAddConverter(typeof(NotAConverter));

			added.Should().BeFalse();
			services.Should().BeEmpty();
		}

		[Fact]
		public void AddCodeGen_ShouldFindConvertersInAssembly() {
			var services = new ServiceCollection();

			services.AddCodeGen(typeof(IntToStringConverter).Assembly);

			services.Any(x => x.ServiceType == typeof(IConvertObject<int, string>) && x.ImplementationType == typeof(IntToStringConverter))
				.Should().BeTrue();
		}
	}
}
