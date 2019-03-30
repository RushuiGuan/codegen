using Autofac;
using Autofac.Extras.Moq;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.Test {
	[TestFixture]
	public class TestAutoMock:TestBase {
		public sealed class ClassA { }
		public sealed class ClassB {
			public ClassA A { get; private set; }
			public ClassB(ClassA classA) {
				A = classA;
			}
		}
		public override void Register(Container container) {
		}

		[Test]
		public void ResolveConcreteClass() {
			using (var scope = GetScope()) {
				var item = scope.Resolve<ClassB>();
				Assert.NotNull(item);
			}
		}
		[Test]
		public void MockConcreteClass() {
			using (var mock = AutoMock.GetStrict()) {
				var item = mock.Create<ClassB>();
				Assert.NotNull(item);
			}
		}
	}
}
