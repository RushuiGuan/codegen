using System.Collections.Generic;
using Albatross.Logging.Core;
using Albatross.CodeGen.Shell.View;
using System;
using System.Collections.ObjectModel;

namespace Albatross.CodeGen.Shell.ViewModel {
	[ViewUsage(typeof(CompositeView))]
	public class CompositeViewModel : WorkspaceViewModel {
		public CompositeViewModel(IWorkspaceService svc, ILogFactory logFactory, IListValues<SourceType> srcTypeList, IListValues<CodeGenerator> generatorList) : base(svc, logFactory) {
			SourceTypeList = srcTypeList;
			GeneratorList = generatorList;
			SetTitle();
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
			SelectedGenerators.Clear();
			if (c.Generators != null) {
				foreach (var item in c.Generators) {
					SelectedGenerators.Add(item);
				}
			}
		}
		public void New() {
			Load(new Composite());
		}

		#region properties
		public IListValues<SourceType> SourceTypeList { get; }
		public IListValues<CodeGenerator> GeneratorList { get; }


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
				}
			}
		}

		public ObservableCollection<string> SelectedGenerators { get; } = new ObservableCollection<string>();

		Type _sourceType;
		public Type SourceType {
			get { return _sourceType; }
			set {
				if (_sourceType != value) {
					_sourceType = value;
					RaisePropertyChanged(nameof(SourceType));
				}
			}
		}
		#endregion

		#region commands

		public RelayCommand<CodeGenerator> AddSelectedCommand { get { return new RelayCommand<CodeGenerator>(AddSelected); } }
		void AddSelected(CodeGenerator args) {
			if (args != null) {
				if (!SelectedGenerators.Contains(args.Name)) {
					SelectedGenerators.Add(args.Name);
				}
			}
		}


		public RelayCommand<string> RemoveSelectedCommand { get { return new RelayCommand<string>(RemoveSelected); } }
		void RemoveSelected(string args) {
			for (int i = 0; i < SelectedGenerators.Count; i++) {
				if (SelectedGenerators[i] == args) {
					SelectedGenerators.RemoveAt(i);
					break;
				}
			}
		}
		#endregion
	}
}
