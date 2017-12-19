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
	public class TableMergeByIDTest : TestBase {
		[Test]
		public void MergeByID() {
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
						Name = "id",
						IdentityColumn = true,
					}
				}
			);
			container.Register<IGetTableColumns>(() => mock.Object, Lifestyle.Singleton);

			var m2 = new Mock<IGetTableIdentityColumn>();
			m2.Setup(args => args.Get(It.IsAny<Table>())).Returns(new Column() { Name = "id", IdentityColumn = true });
			container.Register<IGetTableIdentityColumn>(() => m2.Object, Lifestyle.Singleton);


			StringBuilder sb = new StringBuilder();
			TableMergeByID builder = container.GetInstance<TableMergeByID>();
			builder.Build(sb, new Table { Schema = "schema", Name = "table", }, container.GetInstance<ICodeGeneratorFactory>());
			Assert.AreEqual(@"merge [schema].[table] as dst
using (
	select
		@a as [a],
		@b as [b],
		@id as [id]
) as src on src.[id] = dst.[id]
when matched then update set
	[a] = src.[a],
	[b] = src.[b]
when not matched by target then insert (
	[a],
	[b]
) values (
	src.[a],
	src.[b]
);", sb.ToString());
		}
	}
}
