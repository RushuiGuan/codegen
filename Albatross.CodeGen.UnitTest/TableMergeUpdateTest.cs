using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf = typeof(TableMergeUpdate))]
	public class TableMergeUpdateTest {

		static IEnumerable<TestCaseData> MergeUpdateTestCase() {
			return new TestCaseData[] {
				new TestCaseData(new SymbolTable(), new SqlQueryOption{ ExcludePrimaryKey = false,}){ ExpectedResult="when matched then update set\r\n\t[SyCode] = src.[SyCode],\r\n\t[CuID] = src.[CuID],\r\n\t[OutShares] = src.[OutShares],\r\n\t[CoID] = src.[CoID],\r\n\t[SnID] = src.[SnID]" },
				new TestCaseData(new SymbolTable(), new SqlQueryOption{ ExcludePrimaryKey = true,}){ ExpectedResult="when matched then update set\r\n\t[CuID] = src.[CuID],\r\n\t[OutShares] = src.[OutShares],\r\n\t[CoID] = src.[CoID],\r\n\t[SnID] = src.[SnID]" },
			};
		}

		[TestCaseSource(nameof(MergeUpdateTestCase))]
		public string MergeUpdate(TableMocking mock, SqlQueryOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<TableMergeUpdate>().Build(sb, mock.Table, option, null);
			return sb.ToString();
		}
	}
}
