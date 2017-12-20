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
	public class TableUpdateTest {
		IGetTableIdentityColumn _getIDColumn;
		[OneTimeSetUp]
		public void Setup() {
			var mock = new Mock<IGetTableIdentityColumn>();
			mock.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column() { Name="id", IdentityColumn = true, });
			_getIDColumn = mock.Object;
		}


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
				new Column(){
					Name = "id",
					IdentityColumn = true,
				}
			});
			StringBuilder sb = new StringBuilder();
			new TableUpdate(getColumns.Object, new GetSqlVariableName()).Build(sb, new Table() { Schema = "schema", Name = "table" }, null, null);
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[b] = @b", sb.ToString());
		}
		[Test]
		public void OneColumn() {
			var getColumns = new Mock<IGetTableColumns>();
			getColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "id",
					IdentityColumn = true,
				}
			});
			StringBuilder sb = new StringBuilder();
			new TableUpdate(getColumns.Object, new GetSqlVariableName()).Build(sb, new Table() { Schema = "schema", Name = "table" }, null, null);
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a", sb.ToString());
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
				new Column(){
					Name = "id",
					IdentityColumn = true,
				}
			});
			StringBuilder sb = new StringBuilder();
			new TableUpdate(getColumns.Object, new GetSqlVariableName()).Build(sb,new Table() { Schema = "schema", Name = "table" }, null, null);
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[c] = @c", sb.ToString());
		}
	}
}
