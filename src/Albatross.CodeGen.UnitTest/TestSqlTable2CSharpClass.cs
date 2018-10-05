using Albatross.CodeGen.CSharp.Core;
using Albatross.CodeGen.SimpleInjector;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using Albatross.Test;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestSqlTable2CSharpClass : TestBase {
		public override void Register(Container container) {
			new Pack().RegisterServices(container);
		}

		public static readonly TestCaseData Case1 = new TestCaseData(
			new Procedure {
				Name = "GetCompany",
				Schema = "dbo",
				Parameters = new Albatross.Database.Parameter[] {
					  new Albatross.Database.Parameter {
						   Name = "id",
							Type = new SqlType{ Name = "int"},
					  }
				  },
			}) {
			ExpectedResult = @"",
		};

		public static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				Case1,
			};
		}

		[TestCaseSource(nameof(GetTestCases))]
		public string RunTest(Table table, Class @class) {
			var handle = Get<SqlTable2CSharpClass>();
			StringBuilder sb = new StringBuilder();
			handle.Build(sb, table, @class);
			return sb.ToString();
		}
	}
}
