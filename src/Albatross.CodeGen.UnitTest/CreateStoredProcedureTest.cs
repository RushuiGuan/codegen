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
	public class CreateStoredProcedureTest {


		static IEnumerable<TestCaseData> TableInsertTestCase() {
			return new TestCaseData[] {
				new TestCaseData(SymbolTable.Table, new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult="insert into [test].[Symbol] ([SyCode], [CuID], [OutShares], [CoID], [SnID])\r\nvalues (@syCode, @cuID, @outShares, @coID, @snID)" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Schema = "dbo", Name = "test" }){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, @created, @createdBy, @modified, @modifiedBy)" },
				new TestCaseData(ContactTable.Table, new SqlCodeGenOption{ Schema="dbo", Name="test", Variables={ { "@user", "varchar(100)"} }, Expressions={ { "created", "getdate()"}, {"modified", "getdate()"}, { "createdBy", "@user"},{ "modifiedBy", "@user"} } }){ ExpectedResult="insert into [test].[Contact] ([Domain], [Login], [FirstName], [LastName], [MiddleName], [Gender], [Cell], [Address], [Created], [CreatedBy], [Modified], [ModifiedBy])\r\nvalues (@domain, @login, @firstName, @lastName, @middleName, @gender, @cell, @address, getdate(), @user, getdate(), @user)" },
			};
		}

		[TestCaseSource(nameof(TableInsertTestCase))]
		public string CreateEmptyProcedure(DatabaseObject table, SqlCodeGenOption option) {
			StringBuilder sb = new StringBuilder();
			Ioc.Container.GetInstance<CreateStoredProcedure>().Build(sb, table, option);
			return sb.ToString();
		}


		[TestCaseSource(nameof(TableInsertTestCase))]
		public string CreateProcedure(DatabaseObject table, SqlCodeGenOption option) {
			Container container = Ioc.Container;
			var factory = container.GetInstance<IConfigurableCodeGenFactory>();
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(CreateStoredProcedure),
				SourceType = typeof(DatabaseObject),
				OptionType = typeof(SqlCodeGenOption),
				Name = "create procedure",
			});
			factory.Register(new CodeGenerator {
				GeneratorType = typeof(TableInsert),
				SourceType = typeof(DatabaseObject),
				OptionType = typeof(SqlCodeGenOption),
				Name = "insert",
			});

			CompositeCodeGenerator<DatabaseObject, SqlCodeGenOption> compositeCodeGenerator = new CompositeCodeGenerator<DatabaseObject, SqlCodeGenOption>(factory);
			compositeCodeGenerator.Configure(new Branch(new Leaf("create procedure"), new Leaf("insert")));
			StringBuilder sb = new StringBuilder();
			compositeCodeGenerator.Build(sb, table, option);
			return sb.ToString();
		}
	}
}
