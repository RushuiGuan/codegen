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
	public class TableUpdateByIDTest : CompositeTest {

		[OneTimeSetUp]
		public void Setup() {
			mock_GetTableIDColumn.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column() { Name="id", IdentityColumn = true, });
		}


		[Test]
		public void TwoColumn() {
			mock_GetTableColumns.Reset();
			mock_GetTableColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
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
			ICodeGenerator<Table> builder = container.GetInstance<ICodeGeneratorFactory>().Get<Table>("table_update_by_id");
			StringBuilder sb = new StringBuilder();
			builder.Build(sb, new Table { Schema = "schema", Name = "table" }, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[b] = @b
where
	[id] = @id" , sb.ToString());
		}

		[Test]
		public void OneColumn() {
			mock_GetTableColumns.Reset();
			mock_GetTableColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "id",
					IdentityColumn = true,
				}
			});
			ICodeGenerator<Table> builder = container.GetInstance<ICodeGeneratorFactory>().Get<Table>("table_update_by_id");
			StringBuilder sb = new StringBuilder();
			Dictionary<string, Column> @params = new Dictionary<string, Column>(StringComparer.InvariantCultureIgnoreCase);
			builder.Build(sb, new Table { Schema = "schema", Name = "table" }, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a
where
	[id] = @id", sb.ToString());
		}
		
		[Test]
		public void ComputedColumn() {
			mock_GetTableColumns.Reset();
			mock_GetTableColumns.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
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
			ICodeGenerator<Table> builder = container.GetInstance<ICodeGeneratorFactory>().Get<Table>("table_update_by_id");
			StringBuilder sb = new StringBuilder();
			builder.Build(sb, new Table { Schema = "schema", Name = "table" }, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[c] = @c
where
	[id] = @id", sb.ToString());
		}
	}
}
