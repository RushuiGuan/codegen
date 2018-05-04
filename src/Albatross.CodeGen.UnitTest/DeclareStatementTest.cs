using static Albatross.CodeGen.UnitTest.Extension;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
using NUnit.Framework;
using SimpleInjector;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class DeclareStatementTest {
		static IEnumerable<TestCaseData> BuildDeclareStatementTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByIdentityColumn, }){ ExpectedResult=@"declare
	@cuID as int,
	@outShares as bigint,
	@coID as int,
	@snID as int,
	@syID as int;

update [test].[Symbol] set
	[CuID] = @cuID,
	[OutShares] = @outShares,
	[CoID] = @coID,
	[SnID] = @snID
where
	[SyID] = @syID" },

				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{Filter = FilterOption.ByPrimaryKey, }){ ExpectedResult=@"declare
	@firstName as nvarchar(100),
	@lastName as nvarchar(100),
	@middleName as nvarchar(100),
	@gender as char(20),
	@cell as varchar(50),
	@address as nvarchar(200),
	@created as datetime,
	@createdBy as varchar(100),
	@modified as datetime,
	@modifiedBy as varchar(100),
	@domain as varchar(100),
	@login as varchar(100);

update [test].[Contact] set
	[FirstName] = @firstName,
	[LastName] = @lastName,
	[MiddleName] = @middleName,
	[Gender] = @gender,
	[Cell] = @cell,
	[Address] = @address,
	[Created] = @created,
	[CreatedBy] = @createdBy,
	[Modified] = @modified,
	[ModifiedBy] = @modifiedBy
where
	[Domain] = @domain
	and [Login] = @login" },

				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Variables= new Variable[]{ NonUnicodeStringVariable("user", 100) }, Filter= FilterOption.ByPrimaryKey, Expressions={ { "@created", "getdate()"}, {"@modified", "getdate()"}, { "@createdBy", "@user"},{ "@modifiedBy", "@user"} } }){ ExpectedResult=@"declare
	@user as varchar(100),
	@firstName as nvarchar(100),
	@lastName as nvarchar(100),
	@middleName as nvarchar(100),
	@gender as char(20),
	@cell as varchar(50),
	@address as nvarchar(200),
	@domain as varchar(100),
	@login as varchar(100);

update [test].[Contact] set
	[FirstName] = @firstName,
	[LastName] = @lastName,
	[MiddleName] = @middleName,
	[Gender] = @gender,
	[Cell] = @cell,
	[Address] = @address,
	[Created] = getdate(),
	[CreatedBy] = @user,
	[Modified] = getdate(),
	[ModifiedBy] = @user
where
	[Domain] = @domain
	and [Login] = @login" },
			};
		}

		[TestCaseSource(nameof(BuildDeclareStatementTestCase))]
		public string BuildDeclareStatement(Table table, SqlCodeGenOption option) {
			Container c = Ioc.Container;
			var factory = c.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(new Composite(typeof(Table), typeof(SqlCodeGenOption)) {
				Name = "test",
				Branch = new Branch(new Leaf("sql.declare"), new Branch(new Leaf("newline"), new Leaf("sql.update"), new Leaf("newline"), new Leaf("sql.where.table"))),
				Target = GeneratorTarget.Sql,
			});

			StringBuilder sb = new StringBuilder();
			var handle = factory.Create<Table, SqlCodeGenOption>("test");
			handle.Build(sb, table, option);
			return sb.ToString();
		}

		static IEnumerable<TestCaseData> DeclareStatementWithoutDeclareTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Filter = FilterOption.ByIdentityColumn, }){ ExpectedResult=@"
select
	[SyID],
	[SyCode],
	[CuID],
	[OutShares],
	[CoID],
	[SnID]
from [test].[Symbol]" },
			};
		}

		[TestCaseSource(nameof(DeclareStatementWithoutDeclareTestCase))]
		public string DeclareStatementWithoutDeclare(Table table, SqlCodeGenOption option) {
			Container c = Ioc.Container;
			var factory = c.GetInstance<IConfigurableCodeGenFactory>();
			typeof(DeclareStatement).Assembly.Register(factory);
			factory.RegisterStatic();
			factory.Register(new Composite(typeof(Table), typeof(SqlCodeGenOption)) {
				Name = "test",
				Branch = new Branch(new Leaf("sql.declare"), new Branch(new Leaf("newline"), new Leaf("sql.select.table"))),
				Target = GeneratorTarget.Sql,
			});

			StringBuilder sb = new StringBuilder();
			var handle = factory.Create<Table, SqlCodeGenOption>("test");
			handle.Build(sb, table, option);
			return sb.ToString();
		}
	}
}
