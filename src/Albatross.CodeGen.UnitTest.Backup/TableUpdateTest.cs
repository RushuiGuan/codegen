using static Albatross.CodeGen.UnitTest.Extension;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
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
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ ExcludePrimaryKey = false, }){ ExpectedResult = "update [test].[Symbol] set\r\n\t[SyCode] = @syCode,\r\n\t[CuID] = @cuID,\r\n\t[OutShares] = @outShares,\r\n\t[CoID] = @coID,\r\n\t[SnID] = @snID"},
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ ExcludePrimaryKey = true, }){ ExpectedResult = "update [test].[Symbol] set\r\n\t[CuID] = @cuID,\r\n\t[OutShares] = @outShares,\r\n\t[CoID] = @coID,\r\n\t[SnID] = @snID"},
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ ExcludePrimaryKey = false, }){ ExpectedResult = "update [test].[Contact] set\r\n\t[Domain] = @domain,\r\n\t[Login] = @login,\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = @created,\r\n\t[CreatedBy] = @createdBy,\r\n\t[Modified] = @modified,\r\n\t[ModifiedBy] = @modifiedBy"},
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ ExcludePrimaryKey = true, }){ ExpectedResult = "update [test].[Contact] set\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = @created,\r\n\t[CreatedBy] = @createdBy,\r\n\t[Modified] = @modified,\r\n\t[ModifiedBy] = @modifiedBy"},
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ ExcludePrimaryKey = true, Expressions={ {"@created", "getdate()" }, { "@createdBy", "@user"}, { "@modified","getdate()"}, { "@modifiedBy", "@user"} } }){ ExpectedResult = "update [test].[Contact] set\r\n\t[FirstName] = @firstName,\r\n\t[LastName] = @lastName,\r\n\t[MiddleName] = @middleName,\r\n\t[Gender] = @gender,\r\n\t[Cell] = @cell,\r\n\t[Address] = @address,\r\n\t[Created] = getdate(),\r\n\t[CreatedBy] = @user,\r\n\t[Modified] = getdate(),\r\n\t[ModifiedBy] = @user"},
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Table table, SqlCodeGenOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<TableUpdate>().Build(sb, table, option);
			return sb.ToString();
		}
	}
}
