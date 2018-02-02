using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(TableWhere))]
	public class TableWhereTest {
		static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlQueryOption{ Filter = FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[SyID] = @syID"},
				new TestCaseData(SymbolTable.Table, new SqlQueryOption{ Filter = FilterOption.ByPrimaryKey }){ ExpectedResult = "where\r\n\t[SyCode] = @syCode"},
				new TestCaseData(SymbolTable.Table, new SqlQueryOption{ Filter = FilterOption.ByPrimaryKey | FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[SyID] = @syID\r\n\tand [SyCode] = @syCode"},

				new TestCaseData(ContactTable.Table, new SqlQueryOption{ Filter = FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[ContactID] = @contactID"},
				new TestCaseData(ContactTable.Table, new SqlQueryOption{ Filter = FilterOption.ByPrimaryKey }){ ExpectedResult = "where\r\n\t[Domain] = @domain\r\n\tand [Login] = @login"},
				new TestCaseData(ContactTable.Table, new SqlQueryOption{ Filter = FilterOption.ByPrimaryKey | FilterOption.ByIdentityColumn }){ ExpectedResult = "where\r\n\t[ContactID] = @contactID\r\n\tand [Domain] = @domain\r\n\tand [Login] = @login"},
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(DatabaseObject table, SqlQueryOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<TableWhere>().Build(sb, table, option, null, out var used);
			return sb.ToString();
		}
	}
}
