using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class DeclareStatementTest {
		static IEnumerable<TestCaseData> GetTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlQueryOption()){ ExpectedResult="insert into [test].[Symbol] ([SyCode], [CuID], [OutShares], [CoID], [SnID])\r\nvalues (@syCode, @cuID, @outShares, @coID, @snID)" },
				new TestCaseData(ContactTable.Table, new SqlQueryOption()){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, @created, @createdBy, @modified, @modifiedBy)" },
				new TestCaseData(ContactTable.Table, new SqlQueryOption{ Variables={ { "@user", "varchar(100)"} }, Expressions={ { "created", "getdate()"}, {"modified", "getdate()"}, { "createdBy", "@user"},{ "modifiedBy", "@user"} } }){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, getdate(), @user, getdate(), @user)" },
			};
		}

		[TestCaseSource(nameof(GetTestCase))]
		public string BuildDeclareStatement(DatabaseObject table, SqlQueryOption option) {
			StringBuilder sb = new StringBuilder();
			return sb.ToString();
		}
	}
}
