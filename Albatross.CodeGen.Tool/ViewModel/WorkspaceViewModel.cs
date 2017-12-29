using Albatross.Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Tool.ViewModel {
	public class WorkspaceViewModel : ViewModelBase {
		protected IWorkspaceService WorkspaceService { get; private set; }
		ILog log;
		public WorkspaceViewModel(IWorkspaceService svc, ILogFactory logFactory) {
			WorkspaceService = svc;
			log = logFactory.Get(this);
		}

		public object ID { get; set; }

		public string _title;
		public string Title {
			get { return _title; }
			set {
				if (_title != value) {
					_title = value;
					RaisePropertyChanged(nameof(Title));
				}
			}
		}

		public bool _isSelected;
		public bool IsSelected {
			get { return _isSelected; }
			set {
				if (_isSelected != value) {
					_isSelected = value;
					log.Info($"Workspace({this.GetHashCode()}) Active:{value}");
					RaisePropertyChanged(nameof(IsSelected));
				}
			}
		}


		public RelayCommand CloseCommand {
			get {
				return new RelayCommand(args => WorkspaceService.CloseWorkspace(this));
			}
		}
	}
}
