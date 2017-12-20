using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TableUpdateByPrimaryKeyTest: CompositeTest {
		[OneTimeSetUp]
		public void Setup() {
			mock_GetTableColumns.Reset();
			mock_GetTableColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "b",
				},
				new Column(){
					Name = "c",
				},
				new Column(){
					Name = "d",
				},
			});
		}


		[Test]
		public void SinglePrimaryKeyColumn() {
			mock_GetTablePrimaryKey.Reset();
			mock_GetTablePrimaryKey.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] { new Column() { Name = "d" } });

			ICodeGenerator<Table> builder = container.GetInstance<ICodeGeneratorFactory>().Get<Table>("table_update_by_primarykey");
			StringBuilder sb = new StringBuilder();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[b] = @b,
	[c] = @c
where
	[d] = @d", sb.ToString());
		}
		[Test]
		public void TwoPrimaryKeyColumn() {
			mock_GetTablePrimaryKey.Reset();
			mock_GetTablePrimaryKey.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] { new Column() { Name = "a" }, new Column() { Name = "b"}, });

			ICodeGenerator<Table> builder = container.GetInstance<ICodeGeneratorFactory>().Get<Table>("table_update_by_primarykey");
			StringBuilder sb = new StringBuilder();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"update [schema].[table] set
	[c] = @c,
	[d] = @d
where
	[a] = @a
	and [b] = @b", sb.ToString());
		}
	}
}