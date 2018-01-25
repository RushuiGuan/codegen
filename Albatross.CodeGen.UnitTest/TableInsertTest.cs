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
	public class TableInsertTest {
		[Test]
		public void TwoColumn() {
			var getColumns = new Mock<IGetTableColumns>();
			getColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "b",
				},
			});
			StringBuilder sb = new StringBuilder();
			new TableInsert(getColumns.Object, new GetSqlVariableName()).Build(sb, new Table { Schema = "schema", Name = "table", }, null, null);
			Assert.AreEqual(@"insert into [schema].[table] (
	[a],
	[b]
) values (
	@a,
	@b
);", sb.ToString());
		}
		[Test]
		public void OneColumn() {
			var getColumns = new Mock<IGetTableColumns>();
			getColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				}
			});
			StringBuilder sb = new StringBuilder();
			new TableInsert(getColumns.Object, new GetSqlVariableName()).Build(sb, new Table { Schema = "schema", Name = "table", }, null, null);
			Assert.AreEqual(@"insert into [schema].[table] (
	[a]
) values (
	@a
);", sb.ToString());
		}

		[Test]
		public void IdentityColumn() {
			var getColumns = new Mock<IGetTableColumns>();
			getColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "b",
					IdentityColumn = true
				},
				new Column(){
					Name = "c",
				},
			});
			StringBuilder sb = new StringBuilder();
			new TableInsert(getColumns.Object, new GetSqlVariableName()).Build(sb, new Table { Schema = "schema", Name = "table", }, null, null);
			Assert.AreEqual(@"insert into [schema].[table] (
	[a],
	[c]
) values (
	@a,
	@c
);", sb.ToString());
		}

		[Test]
		public void ComputedColumn() {
			var getColumns = new Mock<IGetTableColumns>();
			getColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "b",
					ComputedColumn = true
				},
				new Column(){
					Name = "c",
				},
			});
			StringBuilder sb = new StringBuilder();
			new TableInsert(getColumns.Object, new GetSqlVariableName()).Build(sb, new Table { Schema = "schema", Name = "table", }, null, null);
			Assert.AreEqual(@"insert into [schema].[table] (
	[a],
	[c]
) values (
	@a,
	@c
);", sb.ToString());
		}
	}
}
