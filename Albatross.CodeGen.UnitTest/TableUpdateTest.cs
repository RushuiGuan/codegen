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
	[TestFixture(TestOf =typeof(TableUpdate))]
	public class TableUpdateTest {
		static IEnumerable<TestCaseData> GetTestCases() {
			return new TestCaseData[] {
				new TestCaseData(new SymbolTable(), new SqlQueryOption{ ExcludePrimaryKey = false, }){ ExpectedResult = "update [test].[Symbol] set\r\n\t[SyCode] = @syCode,\r\n\t[CuID] = @cuID,\r\n\t[OutShares] = @outShares,\r\n\t[CoID] = @coID,\r\n\t[SnID] = @snID"},
				new TestCaseData(new SymbolTable(), new SqlQueryOption{ ExcludePrimaryKey = true, }){ ExpectedResult = "update [test].[Symbol] set\r\n\t[CuID] = @cuID,\r\n\t[OutShares] = @outShares,\r\n\t[CoID] = @coID,\r\n\t[SnID] = @snID"},
				new TestCaseData(new ContactTable(), new SqlQueryOption{ ExcludePrimaryKey = false, }){ ExpectedResult = "update [test].[Contact] set\r\n\t[Domain] = @domain,\r\n\t[Login] = @login,\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = @created,\r\n\t[CreatedBy] = @createdBy,\r\n\t[Modified] = @modified,\r\n\t[ModifiedBy] = @modifiedBy"},
				new TestCaseData(new ContactTable(), new SqlQueryOption{ ExcludePrimaryKey = true, }){ ExpectedResult = "update [test].[Contact] set\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = @created,\r\n\t[CreatedBy] = @createdBy,\r\n\t[Modified] = @modified,\r\n\t[ModifiedBy] = @modifiedBy"},
				new TestCaseData(new ContactTable(), new SqlQueryOption{ ExcludePrimaryKey = true, Variables={ { "@user", "varchar(100)"} }, Expressions={ {"created", "getdate()" }, { "createdBy", "@user"}, { "modified","getdate()"}, { "modifiedBy", "@user"} } }){ ExpectedResult = "update [test].[Contact] set\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = getdate(),\r\n\t[CreatedBy] = @user,\r\n\t[Modified] = getdate(),\r\n\t[ModifiedBy] = @user"},
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(TableMocking mock, SqlQueryOption option) {
			StringBuilder sb = new StringBuilder();
			mock.Build().GetInstance<TableUpdate>().Build(sb, mock.Table, option, null);
			return sb.ToString();
		}
	}
}
