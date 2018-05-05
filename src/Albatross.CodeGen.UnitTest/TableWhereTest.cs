using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(WhereClause))]
	public class TableWhereTest {
		static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[SyID] = @syID"},
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByPrimaryKey }){ ExpectedResult = "where\r\n\t[SyCode] = @syCode"},
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByPrimaryKey | FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[SyID] = @syID\r\n\tand [SyCode] = @syCode"},

				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[ContactID] = @contactID"},
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByPrimaryKey }){ ExpectedResult = "where\r\n\t[Domain] = @domain\r\n\tand [Login] = @login"},
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByPrimaryKey | FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[ContactID] = @contactID\r\n\tand [Domain] = @domain\r\n\tand [Login] = @login"},
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Table table, SqlCodeGenOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<WhereClause>().Generate(sb, table, option);
			return sb.ToString();
		}
	}
}
