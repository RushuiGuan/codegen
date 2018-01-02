using Albatross.Logging;
using log4net.Repository;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	public class BaseCmdlet<T> : PSCmdlet where T:class{
		protected T Handle { get; private set; }

		public BaseCmdlet() {
		}

		protected virtual void GetHandle() {
			Container c = new Container();
			c.Register<IGetLog4NetLoggerRepository, GetDefaultLog4NetLoggerRepository>(Lifestyle.Singleton);
			c.Register<ILoggerRepository>(() => c.GetInstance<IGetLog4NetLoggerRepository>().Get(), Lifestyle.Singleton);

			c.Register<ISourceTypeFactory, SourceTypeFactory>();
			c.Verify();


			Handle = c.GetInstance<T>();
		}
	}
}
