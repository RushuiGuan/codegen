using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CodeGenerator : ViewModelBase {
		public bool UserDefined { get; set; }

		string _name;
		public string Name {
			get { return _name; }
			set {
				if (value != _name) {
					_name = value;
					RaisePropertyChanged(nameof(Name));
				}
			}
		}


		string _category;
		public string Category {
			get { return _category; }
			set {
				if (value != _category) {
					_category = value;
					RaisePropertyChanged(nameof(Category));
				}
			}
		}


		string _description;
		public string Description {
			get { return _description; }
			set {
				if (value != _description) {
					_description = value;
					RaisePropertyChanged(nameof(Description));
				}
			}
		}


		string _target;
		public string Target {
			get { return _target; }
			set {
				if (value != _target) {
					_target = value;
					RaisePropertyChanged(nameof(Target));
				}
			}
		}

		string _type;
		public string Type {
			get { return _type; }
			set {
				if (value != _type) {
					_type = value;
					RaisePropertyChanged(nameof(Type));
				}
			}
		}



		string _location;
		public string Location {
			get { return _location; }
			set {
				if (value != _location) {
					_location = value;
					RaisePropertyChanged(nameof(Location));
				}
			}
		}
	}
}
