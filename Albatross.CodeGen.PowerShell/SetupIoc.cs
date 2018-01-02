using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	public class SetupIoc {

		public IObjectFactory Run() {
			Container c = new Container();
			c.Register<IObjectFactory>(() => new ObjectFactory(c), Lifestyle.Singleton);

			c.Verify();

			Factory = c.GetInstance<IObjectFactory>();
			return Factory;
		}
		public static IObjectFactory Factory { get; private set; }
	}
}
