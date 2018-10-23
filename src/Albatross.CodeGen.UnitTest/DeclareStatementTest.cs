using static Albatross.CodeGen.UnitTest.Extension;
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.CodeGen.UnitTest.Mocking;
using Albatross.Database;
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
			factory.RegisterComposite(new Composite() {
				Name = "test",
				Branch = new Branch(new Leaf("declare statement"), new Branch(new Leaf("newline"), new Leaf("table_update"), new Leaf("newline"), new Leaf("table_where"))),
				Target = GeneratorTarget.Sql,
			});

			StringBuilder sb = new StringBuilder();
			var handle = factory.Create("test");
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
			factory.RegisterAssembly(typeof(DeclareStatement).Assembly);
			factory.RegisterConstant();
			factory.RegisterComposite(new Composite() {
				Name = "test",
				Branch = new Branch(new Leaf("declare statement"), new Branch(new Leaf("newline"), new Leaf("table_select"))),
				Target = GeneratorTarget.Sql,
			});

			StringBuilder sb = new StringBuilder();
			var handle = factory.Create("test");
			handle.Build(sb, table, option);
			return sb.ToString();
		}
	}
}
