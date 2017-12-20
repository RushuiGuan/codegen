using Albatross.Logging;
using Albatross.Logging.Core;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public class ConfigContainer {
		public Container Run() {
			Container container = new Container();
			container.Register<ICodeGeneratorFactory, CfgControlledCodeGeneratorFactory>(Lifestyle.Singleton);
			container.Register<IConfigurableCodeGenFactory, CfgControlledCodeGeneratorFactory>(Lifestyle.Singleton);
			container.RegisterSingleton<IObjectFactory>(new	ObjectFactory(container));
			container.Register<ILogFactory, Log4netLogFactory>(Lifestyle.Singleton);
			new SqlServer.Pack().RegisterServices(container);
			container.RegisterSingleton<SettingRepository>();

			return container;
		}
	}
}
