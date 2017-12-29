using System;
using Albatross.Logging.Core;
using Albatross.CodeGen.Tool.View;

namespace Albatross.CodeGen.Tool.ViewModel {
	[ViewUsage(typeof(AssemblyLocationsView))]
	public class AssemblyLocationsViewModel : WorkspaceViewModel {
		AssemblyLocationRepository assemblyLocationRepository;
		public AssemblyLocationsViewModel(AssemblyLocationRepository repo, IWorkspaceService svc, ILogFactory logFactory) : base(svc, logFactory) {
			assemblyLocationRepository = repo;
			Title = "Assembly Locations";
		}


		string _locations;
		public string Locations {
			get { return _locations; }
			set {
				if (_locations != value) {
					_locations = value;
					RaisePropertyChanged(nameof(Locations));
				}
			}
		}


		public RelayCommand SaveCommand { get { return new RelayCommand(Save); } }
		void Save(object args) {
			AssemblyLocationSetting setting = new AssemblyLocationSetting();
			setting.Locations = _locations?.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			assemblyLocationRepository.Save(setting);
		}


		public RelayCommand RefreshCommand { get { return new RelayCommand(Refresh); } }
		void Refresh(object args) {
			Load();
		}

		public void Load() {
			AssemblyLocationSetting setting = assemblyLocationRepository.Get();
			if (setting?.Locations != null) {
				Locations = string.Join("\n", setting.Locations);
			} else {
				Locations = null;
			}
		}
	}
}
