using System.Linq;
using System.Collections.Generic;
using Albatross.Logging.Core;
using Albatross.CodeGen.Tool.View;
using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Text;

namespace Albatross.CodeGen.Tool.ViewModel {
	[ViewUsage(typeof(CompositeView))]
	public class CompositeViewModel : WorkspaceViewModel {
		ICodeGeneratorFactory _codeGeneratorFactory;

		public CompositeViewModel(IWorkspaceService svc, ILogFactory logFactory, IListValues<SourceType> srcTypeList, ICodeGeneratorFactory codeGeneratorFactory) : base(svc, logFactory) {
			SourceTypeList = srcTypeList;
			this._codeGeneratorFactory = codeGeneratorFactory;
			SetTitle();
			SetAvailableGenerators();
		}

		void SetAvailableGenerators() {
			AvailableGenerators.Clear();
			foreach (var gen in _codeGeneratorFactory.Registrations) {
				if ((SourceType == null || SourceType == gen.SourceType) && (string.IsNullOrEmpty(Target) || string.Equals(Target, gen.Target, StringComparison.InvariantCultureIgnoreCase))) {
					AvailableGenerators.Add(gen.Name);
				}
			}
		}

		void SetTitle() {
			if (string.IsNullOrEmpty(Name)) {
				Title = "Composite:New";
			} else {
				Title = "Composite:" + Name;
			}
		}
		public void Load(Composite c) {
			AutoMapper.Mapper.Map<Composite, CompositeViewModel>(c, this);
			if (c.Generators != null) {
				SelectedGenerators = string.Join("\n", c.Generators);
			}
		}
		public void New() {
			Load(new Composite());
		}

		#region properties
		public IListValues<SourceType> SourceTypeList { get; }

		string _name;
		public string Name {
			get { return _name; }
			set {
				if (_name != value) {
					_name = value;
					RaisePropertyChanged(nameof(Name));
					SetTitle();
				}
			}
		}

		string _category;
		public string Category {
			get { return _category; }
			set {
				if (_category != value) {
					_category = value;
					RaisePropertyChanged(nameof(Category));
				}
			}
		}

		string _description;
		public string Description {
			get { return _description; }
			set {
				if (_description != value) {
					_description = value;
					RaisePropertyChanged(nameof(Description));
				}
			}
		}

		string _target;
		public string Target {
			get { return _target; }
			set {
				if (_target != value) {
					_target = value;
					RaisePropertyChanged(nameof(Target));
					SetAvailableGenerators();
				}
			}
		}


		string _selecedGenerators;
		public string SelectedGenerators {
			get { return _selecedGenerators; }
			set {
				if (_selecedGenerators != value) {
					_selecedGenerators = value;
					RaisePropertyChanged(nameof(SelectedGenerators));
				}
			}
		}


		public ObservableCollection<string> AvailableGenerators { get; } = new ObservableCollection<string>();

		//string _availableGenerators;
		//public string AvailableGenerators {
		//	get { return _availableGenerators; }
		//	set {
		//		if (_availableGenerators != value) {
		//			_availableGenerators = value;
		//			RaisePropertyChanged(nameof(AvailableGenerators));
		//		}
		//	}
		//}

		Type _sourceType;
		public Type SourceType {
			get { return _sourceType; }
			set {
				if (_sourceType != value) {
					_sourceType = value;
					RaisePropertyChanged(nameof(SourceType));
					SetAvailableGenerators();
				}
			}
		}
		#endregion

		#region commands

		public RelayCommand<IList> CopyCommand { get { return new RelayCommand<IList>(Copy); } }
		void Copy(IList args) {
			StringBuilder sb = new StringBuilder();
			if (args != null) {
				foreach (string item in args) {
					sb.AppendLine(item);
				}
			}
			System.Windows.Clipboard.SetText(sb.ToString());
		}

		public RelayCommand AddSelectedCommand {
			get { return new RelayCommand(AddSelected); }
		}
		void AddSelected(object item) {
			if (!string.IsNullOrEmpty(Convert.ToString(item))) {
				if (string.IsNullOrWhiteSpace(SelectedGenerators)) {
					SelectedGenerators = Convert.ToString(item);
				} else {
					SelectedGenerators = SelectedGenerators + "\n" + item;
				}
			}
		}
		#endregion
	}
}
