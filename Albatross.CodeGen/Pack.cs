using SimpleInjector.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Albatross.CodeGen {
	public class Pack : IPackage {
		public void RegisterServices(Container container) {
			container.Register<ICodeGeneratorFactory, ContainerControlledCodeGenFactory>(Lifestyle.Singleton);
		}
	}
}
