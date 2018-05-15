using static Albatross.CodeGen.UnitTest.Extension;
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
	public class CreateStoredProcedureTest {


		static IEnumerable<TestCaseData> CreateEmptyProcedureTestCase() {
			return new TestCaseData[] {
				new TestCaseData(new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult="create procedure [dbo].[test]\r\nas begin\r\nend\r\n" },
				new TestCaseData(new SqlCodeGenOption{ Schema="dbo", Name="test", Parameters = new Parameter[]{ new Parameter{ Name = "user", Type = NonUnicodeString(100), } } }){ ExpectedResult="create procedure [dbo].[test]\r\n\t@user as varchar(100)\r\nas begin\r\nend\r\n" },
			};
		}
		[TestCaseSource(nameof(CreateEmptyProcedureTestCase))]
		public string CreateEmptyProcedure(SqlCodeGenOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<SqlServer.CreateProcedure>().Generate(sb, null, option);
			return sb.ToString();
		}


		static IEnumerable<TestCaseData> CreateProcedureTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult=@"create procedure [dbo].[test]
	@syCode as varchar(100),
	@cuID as int,
	@outShares as bigint,
	@coID as int,
	@snID as int
as begin
	insert into [test].[Symbol] ([SyCode], [CuID], [OutShares], [CoID], [SnID])
	values (@syCode, @cuID, @outShares, @coID, @snID)
end
" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult=@"create procedure [dbo].[test]
	@domain as varchar(100),
	@login as varchar(100),
	@firstName as nvarchar(100),
	@lastName as nvarchar(100),
	@middleName as nvarchar(100),
	@gender as char(20),
	@cell as varchar(50),
	@address as nvarchar(200),
	@created as datetime,
	@createdBy as varchar(100),
	@modified as datetime,
	@modifiedBy as varchar(100)
as begin
	insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])
	values (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, @created, @createdBy, @modified, @modifiedBy)
end
" },
			};
		}

		[TestCaseSource(nameof(CreateProcedureTestCase))]
		public string CreateProcedure(Table table, SqlCodeGenOption option) {
			var factory = Ioc.Container.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(SqlServer.CreateProcedure),
				SourceType = typeof(Table),
				OptionType = typeof(SqlCodeGenOption),
				Name = "sql.procedure.create",
			});
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(InsertStatement),
				SourceType = typeof(Table),
				OptionType = typeof(SqlCodeGenOption),
				Name = "insert",
			});
			table = Ioc.Container.GetInstance<IGetTable>().Get(table.Database, table.Schema, table.Name);
			CompositeCodeGenerator compositeCodeGenerator = new CompositeCodeGenerator(factory);
			compositeCodeGenerator.Configure(new Branch(new Leaf("sql.procedure.create"), new Leaf("insert")));
			StringBuilder sb = new StringBuilder();
			compositeCodeGenerator.Generate(sb, table, option);
			return sb.ToString();
		}
	}
}
