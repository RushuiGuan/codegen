using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using NUnit.Framework;
using System.Collections.Generic;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class ParseSqlTypeTest {
		public static IEnumerable<TestCaseData> SuccessTestCases() {
			return new TestCaseData[] {
				new TestCaseData("datetime2(1)"){ ExpectedResult = new SqlType{ Name = "datetime2", Scale = 1 } },
				new TestCaseData("datetime2"){ ExpectedResult = new SqlType{ Name = "datetime2" } },
				new TestCaseData("varchar"){ ExpectedResult = new SqlType{ Name = "varchar" } },
				new TestCaseData("varchar(100)"){ ExpectedResult = new SqlType{ Name = "varchar", MaxLength = 100 } },
				new TestCaseData("float"){ ExpectedResult = new SqlType{ Name = "float"} },
				new TestCaseData("float(10)"){ ExpectedResult = new SqlType{ Name = "float", Precision = 10} },
				new TestCaseData("decimal"){ ExpectedResult = new SqlType{ Name = "decimal"} },
				new TestCaseData("decimal(20, 10)"){ ExpectedResult = new SqlType{ Name = "decimal", Precision = 20, Scale = 10,} },
			};
		}


		[TestCaseSource(nameof(SuccessTestCases))]
		public SqlType SuccessScenarios(string text) {
			SqlType type = new ParseSqlType().Parse(text);
			return type;
		}
    }
}
