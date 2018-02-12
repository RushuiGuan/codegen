using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	public class BaseCmdlet<T> : PSCmdlet where T:class{
		protected T Handle { get; private set; }

		protected IObjectFactory Factory => SetupIoc.Factory;

		public BaseCmdlet() {
		}

		protected override void BeginProcessing() {
			base.BeginProcessing();
			GetHandle();
			System.Environment.CurrentDirectory = this.SessionState.Path.CurrentFileSystemLocation.Path;
		}
		protected virtual void GetHandle() {
			Handle = Factory.Create<T>();
		}
	}
}
