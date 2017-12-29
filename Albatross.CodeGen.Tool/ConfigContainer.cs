using System.Reflection;
using Albatross.CodeGen.Tool.ViewModel;
using Albatross.Logging;
using Albatross.Logging.Core;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;

namespace Albatross.CodeGen.Tool {
	public class ConfigContainer {
		public Container Run() {
			Container container = new Container();
			container.Register<ICodeGeneratorFactory, CfgControlledCodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IConfigurableCodeGenFactory, CfgControlledCodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<CompositeRepository>(Lifestyle.Singleton);
			container.Register<ScenarioRepository>(Lifestyle.Singleton);
			container.Register<SourceTypeFactory>(Lifestyle.Singleton);

			container.RegisterSingleton<IObjectFactory>(new	ObjectFactory(container));
			container.Register<ILogFactory, Log4netLogFactory>(Lifestyle.Singleton);

			container.RegisterSingleton<IGetTableColumns, GetTableColumns>();
			container.RegisterSingleton<IGetVariableName, GetSqlVariableName>();
			container.RegisterSingleton<IGetTablePrimaryKey, GetTablePrimaryKey>();
			container.RegisterSingleton<IGetTableIdentityColumn, GetTableIdentityColumn>();
			container.RegisterSingleton<IColumnSqlTypeBuilder, ColumnSqlTypeBuilder>();
			container.RegisterCollection<BuiltInColumn>(BuiltInColumns.Items);
			container.RegisterSingleton<IBuiltInColumnFactory, BuiltInColumnFactory>();

			container.RegisterSingleton<AssemblyLocationRepository>();
			container.RegisterSingleton<IViewLocator, ViewLocator>();

			Type genericType = typeof(IListValues<>);
			foreach(Type type in this.GetType().Assembly.GetTypes()){
				ListValueAttribute attrib = type.GetCustomAttribute<ListValueAttribute>();
				if (attrib != null) {
					Type serviceType = genericType.MakeGenericType(attrib.ValueType);
					container.RegisterSingleton(serviceType, type);
				}
			}
			return container;
		}
	}
}
