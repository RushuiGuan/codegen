using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Moq;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TableMergeSelectWithAuditTest : TestBase {
		[Test]
		public void NoAuditFields() {
			Container container = GetContainer();
			var mock = new Mock<IGetTableColumns>();
			mock.Setup(args => args.Get(It.IsAny<Table>())).Returns(
				new Column[] {
					new Column(){
						Name = "a",
					},
					new Column(){
						Name = "b",
					},
				}
			);
			container.Register<IGetTableColumns>(() => mock.Object, Lifestyle.Singleton);

			StringBuilder sb= new StringBuilder();
			TableMergeSelectWithAudit builder = container.GetInstance<TableMergeSelectWithAudit>();
			builder.Build(sb, null, null, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"using (
	select
		@a as [a],
		@b as [b]
) as src", sb.ToString());
		}
		[Test]
		public void FullAuditFields() {
			Container container = GetContainer();
			var mock = new Mock<IGetTableColumns>();
			mock.Setup(args => args.Get(It.IsAny<Table>())).Returns(
				new Column[] {
					new Column(){
						Name = "a",
					},
					new Column(){
						Name = "b",
					},
					new Column(){
						Name = "created",
						DataType = "datetime"
					},
					new Column(){
						Name = "modified",
						DataType = "datetime"
					},
					new Column(){
						Name = "createdby",
						DataType = "varchar",
						MaxLength = 100
					},
					new Column(){
						Name = "modifiedby",
						DataType = "varchar",
						MaxLength = 200
					},
				}
			);
			container.Register<IGetTableColumns>(() => mock.Object, Lifestyle.Singleton);

			StringBuilder sb = new StringBuilder();
			TableMergeSelectWithAudit builder = container.GetInstance<TableMergeSelectWithAudit>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, null, container.GetInstance<ICodeGeneratorFactory>());
		
			Assert.AreEqual(@"using (
	select
		@a as [a],
		@b as [b],
		getdate() as [created],
		getdate() as [modified],
		@user as [createdby],
		@user as [modifiedby]
) as src", sb.ToString());
		}
	}
}
