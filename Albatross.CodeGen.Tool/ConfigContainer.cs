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
			new SqlServer.Pack().RegisterServices(container);
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
