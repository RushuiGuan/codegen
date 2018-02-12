﻿using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
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

			//logging
			container.RegisterSingleton<IObjectFactory>(this);
			/*
			container.RegisterSingleton<GetLog4NetLoggerRepositoryByXmlConfig>();
			container.Register<IGetLog4NetLoggerRepository, GetDefaultLog4NetLoggerRepository>(Lifestyle.Singleton);
			container.Register<ILoggerRepository>(() => container.GetInstance<IGetLog4NetLoggerRepository>().Get(), Lifestyle.Singleton);
			container.Register<ILogFactory, Log4netLogFactory>(Lifestyle.Singleton);
			*/
			container.Register<IGetVariable, SqlVariableMgmt>(Lifestyle.Singleton);
			container.Register<ICreateVariable, SqlVariableMgmt>(Lifestyle.Singleton);

			container.Register<IFactory<SourceType>, SourceTypeFactory>();
			container.Register<IFactory<OptionType>, OptionTypeFactory>();
			container.Register<ICodeGeneratorFactory, CodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IConfigurableCodeGenFactory, CodeGeneratorFactory>(Lifestyle.Singleton);

			container.Register<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>(Lifestyle.Singleton);
			container.Register<IGetTableColumns, GetTableColumns>(Lifestyle.Singleton);
			container.Register<IGetTableIdentityColumn, GetTableIdentityColumn>(Lifestyle.Singleton);
			container.Register<IGetTablePrimaryKey, GetTablePrimaryKey>(Lifestyle.Singleton);
			container.Register<IGetVariableName, GetSqlVariableName>(Lifestyle.Singleton);

			container.Register<ICreateVariable, SqlVariableMgmt>(Lifestyle.Singleton);

			var mock_getTableColumns = new Mock<IGetTableColumns>();
			container.RegisterSingleton<Mock<IGetTableColumns>>(mock_getTableColumns);
			container.RegisterSingleton<IGetTableColumns>(mock_getTableColumns.Object);

			var mock_getTableIdentityColumn = new Mock<IGetTableIdentityColumn>();
			container.RegisterSingleton<Mock<IGetTableIdentityColumn>>(mock_getTableIdentityColumn);
			container.RegisterSingleton<IGetTableIdentityColumn>(mock_getTableIdentityColumn.Object);

			var mock_getTablePrimaryKey = new Mock<IGetTablePrimaryKey>();
			container.RegisterSingleton<Mock<IGetTablePrimaryKey>>(mock_getTablePrimaryKey);
			container.RegisterSingleton<IGetTablePrimaryKey>(mock_getTablePrimaryKey.Object);
		}

		

		public static Lazy<Ioc> _lazy = new Lazy<Ioc>(()=>new Ioc());

		public static Container Container { get { return _lazy.Value.container; } }
	}
}