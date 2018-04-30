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
				new TestCaseData(new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult="create procedure [dbo].[test]\r\nas\r\ngo" },
				new TestCaseData(new SqlCodeGenOption{ Schema="dbo", Name="test", Parameters = new Parameter[]{ new Parameter{ Name = "user", Type = NonUnicodeString(100), } } }){ ExpectedResult="create procedure [dbo].[test]\r\n\t@user as varchar(100)\r\nas\r\ngo" },
			};
		}
		[TestCaseSource(nameof(CreateEmptyProcedureTestCase))]
		public string CreateEmptyProcedure(SqlCodeGenOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<CreateStoredProcedure>().Build(sb, null, option);
			return sb.ToString();
		}


		static IEnumerable<TestCaseData> CreateProcedureTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult="create procedure [dbo].[test]\r\n\t@syCode as varchar(100),\r\n\t@cuID as int,\r\n\t@outShares as bigint,\r\n\t@coID as int,\r\n\t@snID as int\r\nas\r\n\tinsert into [test].[Symbol] ([SyCode], [CuID], [OutShares], [CoID], [SnID])\r\n\tvalues (@syCode, @cuID, @outShares, @coID, @snID)\r\ngo" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult="create procedure [dbo].[test]\r\n\t@domain as varchar(100),\r\n\t@login as varchar(100),\r\n\t@firstName as nvarchar(100),\r\n\t@lastName as nvarchar(100),\r\n\t@middleName as nvarchar(100),\r\n\t@gender as char(20),\r\n\t@cell as varchar(50),\r\n\t@address as nvarchar(200),\r\n\t@created as datetime,\r\n\t@createdBy as varchar(100),\r\n\t@modified as datetime,\r\n\t@modifiedBy as varchar(100)\r\nas\r\n\tinsert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\n\tvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, @created, @createdBy, @modified, @modifiedBy)\r\ngo" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Schema="dbo", Name="test", Parameters = new Parameter[]{ new Parameter{ Name = "user", Type = NonUnicodeString(100), } }, Expressions={ { "@created", "getdate()"}, {"@modified", "getdate()"}, { "@createdBy", "@user"},{ "@modifiedBy", "@user"} } }){ ExpectedResult="create procedure [dbo].[test]\r\n\t@user as varchar(100),\r\n\t@domain as varchar(100),\r\n\t@login as varchar(100),\r\n\t@firstName as nvarchar(100),\r\n\t@lastName as nvarchar(100),\r\n\t@middleName as nvarchar(100),\r\n\t@gender as char(20),\r\n\t@cell as varchar(50),\r\n\t@address as nvarchar(200)\r\nas\r\n\tinsert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\n\tvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, getdate(), @user, getdate(), @user)\r\ngo" },
			};
		}

		[TestCaseSource(nameof(CreateProcedureTestCase))]
		public string CreateProcedure(Table table, SqlCodeGenOption option) {
			Container container = Ioc.Container;
			var factory = container.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(CreateStoredProcedure),
				SourceType = typeof(Table),
				OptionType = typeof(SqlCodeGenOption),
				Name = "create procedure",
			});
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(TableInsert),
				SourceType = typeof(Table),
				OptionType = typeof(SqlCodeGenOption),
				Name = "insert",
			});

			CompositeCodeGenerator<Table, SqlCodeGenOption> compositeCodeGenerator = new CompositeCodeGenerator<Table, SqlCodeGenOption>(factory);
			compositeCodeGenerator.Configure(new Branch(new Leaf("create procedure"), new Leaf("insert")));
			StringBuilder sb = new StringBuilder();
			compositeCodeGenerator.Build(sb, table, option);
			return sb.ToString();
		}
	}
}
