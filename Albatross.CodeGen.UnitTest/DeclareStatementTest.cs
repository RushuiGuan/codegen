using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class DeclareStatementTest {
		static IEnumerable<TestCaseData> GetTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlQueryOption{ Filter = FilterOption.ByIdentityColumn, }){ ExpectedResult=@"declare
	@syID as int;
select
	[SyID],
	[SyCode],
	[CuID],
	[OutShares],
	[CoID],
	[SnID]
from [test].[Symbol]
where
	[SyID] = @syID" },
				new TestCaseData(ContactTable.Table, new SqlQueryOption{Filter = FilterOption.ByPrimaryKey, }){ ExpectedResult=@"declare
	@domain as varchar(100),
	@login as varchar(100);
select
	[ContactID],
	[Domain],
	[Login],
	[FirstName],
	[LastName],
	[MiddleName],
	[Gender],
	[Cell],
	[Address],
	[Created],
	[CreatedBy],
	[Modified],
	[ModifiedBy]
from [test].[Contact]
where
	[Domain] = @domain
	and [Login] = @login" },
				new TestCaseData(ContactTable.Table, new SqlQueryOption{ Variables={ { "@user", "varchar(100)"} }, Expressions={ { "created", "getdate()"}, {"modified", "getdate()"}, { "createdBy", "@user"},{ "modifiedBy", "@user"} } }){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, getdate(), @user, getdate(), @user)" },
			};
		}

		[TestCaseSource(nameof(GetTestCase))]
		public string BuildDeclareStatement(DatabaseObject table, SqlQueryOption option) {
			Container c = Ioc.Container;
			var factory = c.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(typeof(DeclareStatement).Assembly);
			factory.RegisterStatic();
			factory.Register(new Composite<DatabaseObject, SqlQueryOption> {
				Name = "test",
				Branch = new Branch(new Leaf("declare statement"), new Branch(new Leaf("table_select"), new Leaf("newline"), new Leaf("table_where"))),
				Target = GeneratorTarget.Sql,
			});

			StringBuilder sb = new StringBuilder();
			var handle = factory.Create<DatabaseObject, SqlQueryOption>("test");
			IEnumerable<object> used;
			handle.Build(sb, table, option, factory, out used);
			return sb.ToString();
		}
	}
}
