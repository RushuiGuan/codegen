using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using Moq;
using SimpleInjector;
using System;

namespace Albatross.CodeGen.UnitTest {
	public class Ioc : IObjectFactory{
		Container container = new Container();
		public T Create<T>() where T : class {
			return container.GetInstance<T>();
		}

		public object Create(Type type) {
			return container.GetInstance(type);
		}

		private Ioc() {
			Setup();
		}


		void Setup() {
			container.Options.AllowOverridingRegistrations = true;
			container.RegisterSingleton<IObjectFactory>(this);
			new Albatross.CodeGen.SimpleInjector.Pack().RegisterServices(container);
			new Albatross.Database.SqlServer.SimpleInjector.Pack().RegisterServices(container);

			var mock_getTable = new Mock<IGetTable>();
			container.RegisterSingleton<Mock<IGetTable>>(mock_getTable);
			container.RegisterSingleton<IGetTable>(mock_getTable.Object);

			var mock_getProcedure = new Mock<IGetProcedure>();
			container.RegisterSingleton<Mock<IGetProcedure>>(mock_getProcedure);
			container.RegisterSingleton<IGetProcedure>(mock_getProcedure.Object);

			container.GetInstance<Mocking.SymbolTable>().Setup();
			container.GetInstance<Mocking.ContactTable>().Setup();
			container.GetInstance<Mocking.GetCompanyProcedure>().Setup();

			var codeGenFactory = container.GetInstance<IConfigurableCodeGenFactory>();

			typeof(ICodeGeneratorFactory).Assembly.Register(codeGenFactory);
			typeof(ClassGenerator<object>).Assembly.Register(codeGenFactory);
			typeof(SqlCodeGenOption).Assembly.Register(codeGenFactory);
			typeof(Albatross.CodeGen.SqlServer.BuildSqlType).Assembly.Register(codeGenFactory);

			codeGenFactory.RegisterStatic();
		}

		public static Lazy<Ioc> _lazy = new Lazy<Ioc>(()=>new Ioc());
		public static Container Container { get { return _lazy.Value.container; } }
	}
}
