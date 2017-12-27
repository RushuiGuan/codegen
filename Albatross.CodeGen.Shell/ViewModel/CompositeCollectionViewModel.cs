using Albatross.Logging.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CompositeCollectionViewModel : WorkspaceViewModel {
		CompositeRepository _repo;
		public CompositeCollectionViewModel(CompositeRepository repo, IWorkspaceService svc, ILogFactory logFactory):base(svc, logFactory) {
			_repo = repo;
			Title = "Composites";
		}

		public ObservableCollection<Composite> Items { get; } = new ObservableCollection<Composite>();

		public void Load() {
			Items.Clear();
			var stored = _repo.Get();
			foreach (var item in stored) {
				Items.Add(item);
			}
		}

		public RelayCommand RefreshCommand {
			get { return new RelayCommand(args => Load()); }
		}

		public RelayCommand NewCompositeCommand {
			get { return new RelayCommand(args => NewComposite()); }
		}
		void NewComposite() {
			WorkspaceService.Create<CompositeDetailViewModel>(args => args.Load(null));
		}


		Composite _selected;
		public Composite Selected {
			get { return _selected; }
			set {
				if (value != _selected) {
					_selected = value;
					RaisePropertyChanged(nameof(Selected));
				}
			}
		}

		public RelayCommand RunCommand {
			get { return new RelayCommand(args => Run(args)); }
		}
		void Run(object args) {
			Composite c = args as Composite;
			if(c!= null) {
				CompositeCodeGenerator gen = new CompositeCodeGenerator(c);
				WorkspaceService.Create<CodeGenerationViewModel>(vm => vm.Init(gen));
			}
		}

		public RelayCommand EditCommand {
			get { return new RelayCommand(args => Edit(args)); }
		}
		void Edit(object args) {
			if (args is Composite) {
				WorkspaceService.Create<CompositeDetailViewModel>(null, ((Composite)args).Name);
			}
		}
	}
}
