using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TableMergeTest : CompositeTest {

		[OneTimeSetUp]
		public void Setup() {
			mock_GetTableIDColumn.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column() {
				Name = "id",
				IdentityColumn = true,
			});
			mock_GetTableColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(
				new Column[] {
					new Column() { Name = "id", IdentityColumn = true, OrdinalPosition = 0 },
					new Column() { Name = "a", OrdinalPosition = 1 },
					new Column() { Name = "b", OrdinalPosition = 2 },
					new Column() { Name = "c", OrdinalPosition = 3 },
					new Column() { Name = "d", OrdinalPosition = 4 },
				}
			);
			mock_GetTablePrimaryKey.Setup(args => args.Get(It.IsAny<Table>())).Returns(
				new Column[] {
					new Column() { Name = "a", },
					new Column() { Name = "b", },
				}
			);
		}


		[Test]
		public void MergeSelect() {
			StringBuilder sb = new StringBuilder();
			TableMergeSelect builder = container.GetInstance<TableMergeSelect>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"using (
	select
		@id as [id],
		@a as [a],
		@b as [b],
		@c as [c],
		@d as [d]
) as src", sb.ToString());
		}

		[Test]
		public void MergeUpdate() {
			StringBuilder sb = new StringBuilder();
			TableMergeUpdate builder = container.GetInstance<TableMergeUpdate>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"when matched then update set
	[a] = src.[a],
	[b] = src.[b],
	[c] = src.[c],
	[d] = src.[d]", sb.ToString());
		}

		[Test]
		public void MergeUpdateExcludePrimaryKey() {
			StringBuilder sb = new StringBuilder();
			TableMergeUpdateExcludePrimaryKey builder = container.GetInstance<TableMergeUpdateExcludePrimaryKey>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"when matched then update set
	[c] = src.[c],
	[d] = src.[d]", sb.ToString());
		}

		[Test]
		public void MergeInsert() {
			StringBuilder sb = new StringBuilder();
			TableMergeInsert builder = container.GetInstance<TableMergeInsert>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"when not matched by target then insert (
	[a],
	[b],
	[c],
	[d]
) values (
	src.[a],
	src.[b],
	src.[c],
	src.[d]
)", sb.ToString());
		}

		[Test]
		public void MergeJoinByID() {
			StringBuilder sb = new StringBuilder();
			TableMergeJoinByID builder = container.GetInstance<TableMergeJoinByID>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"on src.[id] = dst.[id]", sb.ToString());
		}

		[Test]
		public void MergeJoinByPrimaryKey() {
			StringBuilder sb = new StringBuilder();
			TableMergeJoinByPrimaryKey builder = container.GetInstance<TableMergeJoinByPrimaryKey>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"on src.[a] = dst.[a]
	and src.[b] = dst.[b]", sb.ToString());
		}

	
		[Test]
		public void MergeByPrimaryKey() {
			StringBuilder sb = new StringBuilder();
			TableMergeByPrimaryKey builder = container.GetInstance<TableMergeByPrimaryKey>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"merge [schema].[table] as dst
using (
	select
		@id as [id],
		@a as [a],
		@b as [b],
		@c as [c],
		@d as [d]
) as src on src.[a] = dst.[a]
	and src.[b] = dst.[b]
when matched then update set
	[c] = src.[c],
	[d] = src.[d]
when not matched by target then insert (
	[a],
	[b],
	[c],
	[d]
) values (
	src.[a],
	src.[b],
	src.[c],
	src.[d]
);", sb.ToString());
		}
	}
}
