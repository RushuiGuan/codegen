using static Albatross.CodeGen.UnitTest.Extension;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Albatross.Database;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TableInsertTest {

		static IEnumerable<TestCaseData> TableInsertTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption()){ ExpectedResult="insert into [test].[Symbol] ([SyCode], [CuID], [OutShares], [CoID], [SnID])\r\nvalues (@syCode, @cuID, @outShares, @coID, @snID)" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption()){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, @created, @createdBy, @modified, @modifiedBy)" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Expressions={ { "@created", "getdate()"}, {"@modified", "getdate()"}, { "@createdBy", "@user"},{ "@modifiedBy", "@user"} } }){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, getdate(), @user, getdate(), @user)" },
			};
		}

		[TestCaseSource(nameof(TableInsertTestCase))]
		public string TableInsert(Table table, SqlCodeGenOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<TableInsert>().Build(sb, table, option);
			return sb.ToString();
		}
	}
}
