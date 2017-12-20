using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class WorkspaceViewModel : ViewModelBase {
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
					RaisePropertyChanged(nameof(IsSelected));
				}
			}
		}
	}
}
