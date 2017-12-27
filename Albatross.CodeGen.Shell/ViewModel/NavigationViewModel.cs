using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Logging.Core;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class NavigationViewModel : WorkspaceViewModel {
		public NavigationViewModel(IWorkspaceService svc, ILogFactory logFactory) : base(svc, logFactory) {
			Title = "Navigation";
		}
		public RelayCommand CodeGeneratorsCommand { get { return new RelayCommand(args => WorkspaceService.Create<CodeGeneratorCollectionViewModel>(vm => vm.Load(), 0)); } }

		public RelayCommand CompositesCommand { get { return new RelayCommand(args => WorkspaceService.Create<CompositeCollectionViewModel>(vm => vm.Load(), 0)); } }
	}
}
