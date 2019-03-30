using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Model;
using Albatross.CodeGen.Renderer;
using Albatross.CodeGen.SimpleInjector;
using Albatross.Database;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest
{
    [TestFixture]
	public class TestSqlTable2CSharpClass : TestBase {
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public static readonly TestCaseData Case1 = new TestCaseData(
			new Table {
				Name = "Company",
				Schema = "dbo",
				Columns = new Column[] {
					new Column{
						Name = "Id",
						Type = new SqlType{ Name = "int"},
					},
					new Column{
						Name = "Type",
						Type = new SqlType{ Name = "int"},
					},
				}
			},
			new Class {
				Namespace = "Albatross.Test",
				Properties = new Property[] {
					new Property("Type") {
						Type = new DotNetType("CompanyType"),
					}
				}
			}) {
			ExpectedResult = @"namespace Albatross.Test {
	public class Company {
		public int Id { get; set; }
		public CompanyType Type { get; set; }
	}
}",
		};

		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				Case1,
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string RunTest(Table table, Class @class) {
			var handle = Get<RenderCSharpClass>();
			StringBuilder sb = new StringBuilder();
			handle.Build(sb, table, @class);
			return sb.ToString();
		}
	}
}
