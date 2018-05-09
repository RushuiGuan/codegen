using Albatross.CodeGen.Generation;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf = typeof(MergeUpdate))]
	public class TableMergeUpdateTest {

		static IEnumerable<TestCaseData> MergeUpdateTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption()){ ExpectedResult="when matched then update set\r\n\t[CuID] = src.[CuID],\r\n\t[OutShares] = src.[OutShares],\r\n\t[CoID] = src.[CoID],\r\n\t[SnID] = src.[SnID]" },
			};
		}

		[TestCaseSource(nameof(MergeUpdateTestCase))]
		public string MergeUpdate(Table table, SqlCodeGenOption option) {
			table = Ioc.Container.GetInstance<IGetTable>().Get(table.Database, table.Schema, table.Name);
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<MergeUpdate>().Generate(sb, table, option);
			return sb.ToString();
		}
	}
}
