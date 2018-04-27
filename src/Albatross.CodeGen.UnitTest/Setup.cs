using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Database;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[SetUpFixture]
	public class Setup {
		[OneTimeSetUp]
		public void Run() {

			Ioc.Container.GetInstance<Mocking.SymbolTable>().Setup();
			Ioc.Container.GetInstance<Mocking.ContactTable>().Setup();
			Ioc.Container.GetInstance<Mocking.GetCompanyProcedure>().Setup();

			var codeGenFactory = Ioc.Container.GetInstance<IConfigurableCodeGenFactory>();

			typeof(Albatross.CodeGen.ICodeGeneratorFactory).Assembly.Register(codeGenFactory);
			typeof(Albatross.CodeGen.CSharp.ClassGenerator<object>).Assembly.Register(codeGenFactory);
			typeof(Albatross.CodeGen.Database.SqlCodeGenOption).Assembly.Register(codeGenFactory);
			typeof(Albatross.CodeGen.SqlServer.BuildSqlType).Assembly.Register(codeGenFactory);



			codeGenFactory.RegisterStatic();
		}
	}
}
