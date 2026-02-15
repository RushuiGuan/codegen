using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Albatross.CodeGen.WebClient.UnitTest {
	public class TestRouteAndSettingsExtensions {
		[Fact]
		public void GetRouteSegments_ShouldSplitTextAndParameters() {
			var segments = "api/{id}/detail/{*slug}".GetRouteSegments().ToArray();

			segments.Should().HaveCount(4);
			segments[0].Should().BeOfType<RouteTextSegment>();
			segments[1].Should().BeOfType<RouteParameterSegment>();
			segments[2].Should().BeOfType<RouteTextSegment>();
			segments[3].Should().BeOfType<RouteParameterSegment>();
			segments[0].Text.Should().Be("api/");
			segments[1].Text.Should().Be("id");
			segments[2].Text.Should().Be("/detail/");
			segments[3].Text.Should().Be("slug");
		}

		[Fact]
		public void GetRouteSegments_ShouldParseTypeConstraints() {
			var segments = "api/{name:string}/{value:int}".GetRouteSegments().ToArray();

			segments.Should().HaveCount(4);
			((RouteParameterSegment)segments[1]).Text.Should().Be("name");
			((RouteParameterSegment)segments[3]).Text.Should().Be("value");
		}

		[Fact]
		public void GetRouteSegments_ShouldKeepSingleCharacterTrailingText() {
			var segments = "api/{id}x".GetRouteSegments().ToArray();

			segments.Should().HaveCount(3);
			segments[2].Should().BeOfType<RouteTextSegment>();
			segments[2].Text.Should().Be("x");
		}

		[Fact]
		public void RouteParameterSegment_RequiredParameterInfo_ShouldThrowWhenUnset() {
			var segment = new RouteParameterSegment("id");

			var action = () => segment.RequiredParameterInfo;

			action.Should().Throw<System.InvalidOperationException>();
		}

		[Fact]
		public void CodeGenSettingsExtensions_ShouldDropEmptyFilters() {
			var settings = new CodeGenSettings {
				ControllerFilters = [
					new SymbolFilterPatterns(),
					new SymbolFilterPatterns { Include = "Controller$" }
				],
				ControllerMethodFilters = [
					new SymbolFilterPatterns { Exclude = "Filtered$" },
					new SymbolFilterPatterns()
				],
				DtoEnumFilters = [
					new SymbolFilterPatterns(),
					new SymbolFilterPatterns { Include = "^Test\\.Dto\\." }
				]
			};

			settings.ControllerFilters().Should().HaveCount(1);
			settings.ControllerMethodFilters().Should().HaveCount(1);
			settings.DtoFilters().Should().HaveCount(1);
		}
	}
}
