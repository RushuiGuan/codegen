using NUnit.Framework;
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
	}
}
