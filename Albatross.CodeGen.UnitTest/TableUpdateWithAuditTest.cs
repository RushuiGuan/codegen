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
	public class TableUpdateWithAuditTest : TestBase {
		[Test]
		public void NoAuditFields() {
			var mock = new Mock<IGetTableColumns>();
			mock.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "b",
				},
			});
			Container container = GetContainer();
			container.Register<IGetTableColumns>(() => mock.Object, Lifestyle.Singleton);

			StringBuilder sb = new StringBuilder();
			TableUpdateWithAudit handle = container.GetInstance<TableUpdateWithAudit>();
			string result = handle.Build(sb, new Table { Schema = "schema", Name = "table", }, null).ToString();
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[b] = @b", result);
		}

		[Test]
		public void MismatchedAuditFields() {
			var mock = new Mock<IGetTableColumns>();
			mock.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
				},
				new Column(){
					Name = "b",
				},
				new Column(){
					Name = "created",
					DataType = "string"
				}
			});
			Container container = GetContainer();
			container.Register<IGetTableColumns>(() => mock.Object, Lifestyle.Singleton);

			StringBuilder sb = new StringBuilder();
			TableUpdateWithAudit handle = container.GetInstance<TableUpdateWithAudit>();
			string result = handle.Build(sb, new Table { Schema = "schema", Name = "table", }, null).ToString();
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[b] = @b,
	[created] = @created", result);
		}

		[Test]
		public void FullAuditFields() {
			var mock = new Mock<IGetTableColumns>();
			mock.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column[] {
				new Column(){
					Name = "a",
					DataType = "varchar",
					MaxLength = 10,
				},
				new Column(){
					Name = "b",
					DataType = "varchar",
					MaxLength = 20,
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
					Name = "createdBy",
					DataType = "varchar",
					MaxLength = 100
				},
				new Column(){
					Name = "modifiedBy",
					DataType = "varchar",
					MaxLength = 200
				},
			});
			Container container = GetContainer();
			container.Register<IGetTableColumns>(() => mock.Object, Lifestyle.Singleton);

			StringBuilder sb = new StringBuilder();
			TableUpdateWithAudit handle = container.GetInstance<TableUpdateWithAudit>();
			sb = handle.Build(sb, new Table { Schema = "schema", Name = "table", }, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"update [schema].[table] set
	[a] = @a,
	[b] = @b,
	[modified] = getdate(),
	[modifiedBy] = @user", sb.ToString());
		}
	}
}