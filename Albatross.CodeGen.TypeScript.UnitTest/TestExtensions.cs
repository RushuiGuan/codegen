using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.TypeScript.TypeConversions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Albatross.CodeGen.TypeScript.UnitTest;

public class TestExtensions {
	[Fact]
	public void ParseIdentifierName_ShouldParseSimpleAndQualifiedForms() {
		var simple = "MyType".ParseIdentifierName();
		var qualified = "MyType,./models".ParseIdentifierName();

		simple.Should().BeOfType<IdentifierNameExpression>();
		qualified.Should().BeOfType<QualifiedIdentifierNameExpression>();
		((QualifiedIdentifierNameExpression)qualified).Source.Source.Should().Be("./models");
	}

	[Fact]
	public void ParseIdentifierName_ShouldThrowForInvalidInput() {
		var action = () => " ,./models".ParseIdentifierName();

		action.Should().Throw<ArgumentException>();
	}

	[Fact]
	public void ToPromiseAndToObservable_ShouldBeIdempotent() {
		var number = Defined.Types.Numeric();

		var promise1 = number.ToPromise();
		var promise2 = promise1.ToPromise();
		var observable1 = number.ToObservable();
		var observable2 = observable1.ToObservable();

		promise1.IsPromise().Should().BeTrue();
		promise2.IsPromise().Should().BeTrue();
		observable1.IsObservable().Should().BeTrue();
		observable2.IsObservable().Should().BeTrue();
		promise1.Should().BeEquivalentTo(promise2);
		observable1.Should().BeEquivalentTo(observable2);
	}

	[Fact]
	public void AddTypeConverters_ShouldNotDuplicateRegistrations() {
		var services = new ServiceCollection();

		services.AddTypeConverters(typeof(ConvertType).Assembly);
		var once = services.Count(x => x.ServiceType == typeof(ITypeConverter));

		services.AddTypeConverters(typeof(ConvertType).Assembly);
		var twice = services.Count(x => x.ServiceType == typeof(ITypeConverter));

		twice.Should().Be(once);
		services.Should().Contain(x => x.ServiceType == typeof(ITypeConverter) && x.ImplementationType == typeof(StringTypeConverter));
	}
}
