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

		public static TestCaseData[] GetTestCase() {
			return new TestCaseData[] {
				new TestCaseData(new ParamCollection{
					["@a"] = new Column(){ DataType = "int",},
					["@b"] = new Column(){ DataType = "int",}
				}){ ExpectedResult = @"declare
	@a as int,
	@b as int;" },
				new TestCaseData(new ParamCollection{
					["@a"] = new Column(){ DataType = "int",},
				}){ ExpectedResult = @"declare
	@a as int;" },
			};
		}

		[TestCaseSource(nameof(GetTestCase))]
		public string Build(ParamCollection @params) {
			DeclareStatement handle = Container.GetInstance<DeclareStatement>();
			return handle.Build(new StringBuilder(), @params, null, null).ToString();
		}
	}
}
