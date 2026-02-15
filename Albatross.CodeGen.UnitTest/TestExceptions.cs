using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.UnitTest {
	public class TestExceptions {
		[Fact]
		public void CodeGenException_ShouldPreserveMessage() {
			var ex = new CodeGenException("boom");

			ex.Message.Should().Be("boom");
		}

		[Fact]
		public void ConversionException_DefaultCtor_ShouldWork() {
			var ex = new ConversionException();

			ex.Should().NotBeNull();
		}
	}
}
