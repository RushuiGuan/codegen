using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[SetUpFixture]
	public class Setup {
		[OneTimeSetUp]
		public void Run() {
			Ioc.Container.GetInstance<Mocking.SymbolTable>().Setup();
			Ioc.Container.GetInstance<Mocking.ContactTable>().Setup();
		}
	}
}
