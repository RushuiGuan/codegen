using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using Albatross.Logging;
using Albatross.Logging.Core;
using log4net.Repository;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Albatross.CodeGen.UnitTest {
	public class SetupIoc {

		public Container Get() {
			Container c = new Container();
			c.Options.AllowOverridingRegistrations = true;

			//logging
			c.RegisterSingleton<GetLog4NetLoggerRepositoryByXmlConfig>();
			c.Register<IGetLog4NetLoggerRepository, GetDefaultLog4NetLoggerRepository>(Lifestyle.Singleton);
			c.Register<ILoggerRepository>(() => c.GetInstance<IGetLog4NetLoggerRepository>().Get(), Lifestyle.Singleton);
			c.Register<ILogFactory, Log4netLogFactory>(Lifestyle.Singleton);

			c.Register<JsonFileRepository<CodeGenSetting>>(Lifestyle.Singleton);
			c.Register<JsonFileRepository<Composite>>(Lifestyle.Singleton);
			c.Register<IFactory<IEnumerable<SourceType>>, SourceTypeFactory>();
			c.Register<IFactory<IEnumerable<OptionType>>, OptionTypeFactory>();
			c.Register<IGetDefaultRepoFolder, GetDefaultRepoFolder>(Lifestyle.Singleton);
			c.Register<ICodeGeneratorFactory, CfgControlledCodeGeneratorFactory>(Lifestyle.Singleton);
			c.Register<IConfigurableCodeGenFactory, CfgControlledCodeGeneratorFactory>(Lifestyle.Singleton);
			c.Register<ISaveFile<CodeGenSetting>, CodeGenSettingFactory>(Lifestyle.Singleton);
			c.Register<IGetFiles, GetFiles>(Lifestyle.Singleton);

			c.Register<IFactory<IEnumerable<Composite>>, CompositeFactory>(Lifestyle.Singleton);
			c.Register<IFactory<CodeGenSetting>, CodeGenSettingFactory>(Lifestyle.Singleton);
			c.Register<IFactory<IEnumerable<Assembly>>, GetAssembly>(Lifestyle.Singleton);

			c.Register<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>(Lifestyle.Singleton);
			c.Register<IGetTableColumns, GetTableColumns>(Lifestyle.Singleton);
			c.Register<IGetTableIdentityColumn, GetTableIdentityColumn>(Lifestyle.Singleton);
			c.Register<IGetTablePrimaryKey, GetTablePrimaryKey>(Lifestyle.Singleton);
			c.Register<IGetVariableName, GetSqlVariableName>(Lifestyle.Singleton);
			c.Register<IBuiltInColumnFactory, BuiltInColumnFactory>(Lifestyle.Singleton);
			c.RegisterCollection<BuiltInColumn>(BuiltInColumns.Items);

			return c;
		}
	}
}
