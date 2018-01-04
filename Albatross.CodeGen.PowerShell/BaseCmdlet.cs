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

		protected IObjectFactory Factory => SetupIoc.Factory;

		public BaseCmdlet() {
		}

		protected override void BeginProcessing() {
			base.BeginProcessing();
			GetHandle();
		}
		protected virtual void GetHandle() {
			Handle = Factory.Create<T>();
		}
	}
}
