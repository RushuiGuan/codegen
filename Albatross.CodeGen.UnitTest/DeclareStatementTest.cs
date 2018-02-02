using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class DeclareStatementTest : TestBase{
		Container Container;

		[OneTimeSetUp]
		public void Setup() {
			Container = GetContainer();
			Container.Verify();
		}

		public string Build(DatabaseObject @params) {
			DeclareStatement handle = Container.GetInstance<DeclareStatement>();
			return null;
			//return handle.Build(new StringBuilder(), @params, null, null).ToString();
		}
	}
}
