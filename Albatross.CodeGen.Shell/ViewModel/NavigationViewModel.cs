using Albatross.Logging.Core;
using Albatross.CodeGen.Shell.View;

namespace Albatross.CodeGen.Shell.ViewModel {
	[ViewUsage(typeof(NavigationView))]
	public class NavigationViewModel : WorkspaceViewModel {
		public NavigationViewModel(IWorkspaceService svc, ILogFactory logFactory) : base(svc, logFactory) {
			Title = "Navigation";
		}
		public RelayCommand CodeGeneratorsCommand { get { return new RelayCommand(args => WorkspaceService.Create<CodeGeneratorCollectionViewModel>(vm => vm.Load(), 0)); } }

		public RelayCommand CompositesCommand { get { return new RelayCommand(args => WorkspaceService.Create<CompositeCollectionViewModel>(vm => vm.Load(), 0)); } }


		public RelayCommand AssemblyLocationsCommand { get { return new RelayCommand(AssemblyLocations); } }
		void AssemblyLocations(object args) {
			WorkspaceService.Create<AssemblyLocationsViewModel>(vm=>vm.Load(), 0);
		}
	}
}
