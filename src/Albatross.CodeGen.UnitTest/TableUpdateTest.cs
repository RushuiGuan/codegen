using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(UpdateStatement))]
	public class TableUpdateTest {
		static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption()){ ExpectedResult = "update [test].[Symbol] set\r\n\t[CuID] = @cuID,\r\n\t[OutShares] = @outShares,\r\n\t[CoID] = @coID,\r\n\t[SnID] = @snID"},
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption()){ ExpectedResult = "update [test].[Contact] set\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = @created,\r\n\t[CreatedBy] = @createdBy,\r\n\t[Modified] = @modified,\r\n\t[ModifiedBy] = @modifiedBy"},
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Table table, SqlCodeGenOption option) {
			table = Ioc.Container.GetInstance<IGetTable>().Get(table.Database, table.Schema, table.Name);
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<UpdateStatement>().Generate(sb, new Dictionary<string, string>(), table, option);
			return sb.ToString();
		}
	}
}
