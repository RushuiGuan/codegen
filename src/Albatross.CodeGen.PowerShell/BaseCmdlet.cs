using Autofac;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	public abstract class BaseCmdlet<T> : PSCmdlet where T:class{
		protected T EntryObject { get; private set; }
		protected IContainer Container;
		protected abstract void RegisterContainer(ContainerBuilder builder);

		protected override void BeginProcessing() {
			base.BeginProcessing();
			System.Environment.CurrentDirectory = this.SessionState.Path.CurrentFileSystemLocation.Path;
			ContainerBuilder builder = new ContainerBuilder();
			RegisterContainer(builder);
			Container = builder.Build();
			EntryObject = Container.Resolve<T>();
		}

		protected override void EndProcessing() {
			base.EndProcessing();
			Container.Dispose();
		}
	}
}
