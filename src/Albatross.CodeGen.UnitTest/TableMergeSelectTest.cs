using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf = typeof(MergeSelect))]
	public class TableMergeSelectTest {

		static IEnumerable<TestCaseData> MergeSelectTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption()){ ExpectedResult="using (\r\n\tselect\r\n\t\t@syID as [SyID],\r\n\t\t@syCode as [SyCode],\r\n\t\t@cuID as [CuID],\r\n\t\t@outShares as [OutShares],\r\n\t\t@coID as [CoID],\r\n\t\t@snID as [SnID]\r\n) as src" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption()){ ExpectedResult="using (\r\n\tselect\r\n\t\t@contactID as [ContactID],\r\n\t\t@domain as [Domain],\r\n\t\t@login as [Login],\r\n\t\t@firstName as [FirstName],\r\n\t\t@lastName as [LastName],\r\n\t\t@middleName as [MiddleName],\r\n\t\t@gender as [Gender],\r\n\t\t@cell as [Cell],\r\n\t\t@address as [Address],\r\n\t\t@created as [Created],\r\n\t\t@createdBy as [CreatedBy],\r\n\t\t@modified as [Modified],\r\n\t\t@modifiedBy as [ModifiedBy]\r\n) as src" },
			};
		}

		[TestCaseSource(nameof(MergeSelectTestCase))]
		public string MergeSelect(Table table, SqlCodeGenOption option) {
			table = Ioc.Container.GetInstance<IGetTable>().Get(table.Database, table.Schema, table.Name);
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<MergeSelect>().Generate(sb, table, option);
			return sb.ToString();
		}
	}
}
