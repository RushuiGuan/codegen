using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
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

			var codeGenFactory = Ioc.Container.GetInstance<IConfigurableCodeGenFactory>();
			var srcTypeFactory = Ioc.Container.GetInstance<SourceTypeFactory>();
			var optionTypeFactory = Ioc.Container.GetInstance<OptionTypeFactory>();

			Assembly.Load("Albatross.CodeGen").Register(codeGenFactory, srcTypeFactory, optionTypeFactory);
			Assembly.Load("Albatross.CodeGen.CSharp").Register(codeGenFactory, srcTypeFactory, optionTypeFactory);
			Assembly.Load("Albatross.CodeGen.Database").Register(codeGenFactory, srcTypeFactory, optionTypeFactory);
			Assembly.Load("Albatross.CodeGen.SqlServer").Register(codeGenFactory, srcTypeFactory, optionTypeFactory);

			codeGenFactory.RegisterStatic();
		}
	}
}
