using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using SimpleInjector;
using System;

namespace Albatross.CodeGen.PowerShell {
	public class SetupIoc {

		private IObjectFactory Run() {
			Container c = new Container();
			c.Register<IObjectFactory>(() => new ObjectFactory(c), Lifestyle.Singleton);
			/*
			//logging
			c.RegisterSingleton<GetLog4NetLoggerRepositoryByXmlConfig>();
			c.Register<IGetLog4NetLoggerRepository, GetDefaultLog4NetLoggerRepository>(Lifestyle.Singleton);
			c.Register<ILoggerRepository>(() => c.GetInstance<IGetLog4NetLoggerRepository>().Get(), Lifestyle.Singleton);
			c.Register<ILogFactory, Log4netLogFactory>(Lifestyle.Singleton);
			*/

			c.Register<IFactory<SourceType>, SourceTypeFactory>(Lifestyle.Singleton);
			c.Register<IFactory<OptionType>, OptionTypeFactory>(Lifestyle.Singleton);
			c.Register<ICodeGeneratorFactory, CodeGeneratorFactory>(Lifestyle.Singleton);

			c.Register<IRunCodeGenerator, RunCodeGenerator>(Lifestyle.Singleton);
			c.Register<IConfigurableCodeGenFactory, CodeGeneratorFactory>(Lifestyle.Singleton);

			c.Register<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>(Lifestyle.Singleton);
			c.Register<IGetTableColumn, GetTableColumn>(Lifestyle.Singleton);
			c.Register<IGetTableIdentityColumn, GetTableIdentityColumn>(Lifestyle.Singleton);
			c.Register<IGetTablePrimaryKey, GetTablePrimaryKey>(Lifestyle.Singleton);
			c.Register<IGetVariableName, GetSqlVariableName>(Lifestyle.Singleton);
			c.Register<ICreateVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			c.Register<IGetReflectionOnlyType, GetReflectionOnlyType>(Lifestyle.Singleton);

			c.Verify();
			return c.GetInstance<IObjectFactory>();
		}

		static Lazy<IObjectFactory> _lazy = new Lazy<IObjectFactory>(() => new SetupIoc().Run());
		public static IObjectFactory Factory { get; private set; } = _lazy.Value;
	}
}
