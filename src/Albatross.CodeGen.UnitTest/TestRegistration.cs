using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestRegistration {
		[Test]
		public void Test() {
			new Albatross.CodeGen.PowerShell.RegisterAssembly().Invoke();
		}

		[Test]
		public void TestSimpleInjectorRegistration() {
			new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(new Container());
		}
	}
}
