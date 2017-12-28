using Albatross.CodeGen.Shell.View;
using Albatross.Logging.Core;
using System;
using System.Collections.ObjectModel;

namespace Albatross.CodeGen.Shell.ViewModel {
	[ViewUsage(typeof(CodeGeneratorCollectionView))]
	public class CodeGeneratorCollectionViewModel : WorkspaceViewModel {
		IConfigurableCodeGenFactory _codeGenFactory;

		public CodeGeneratorCollectionViewModel(IConfigurableCodeGenFactory codeGenFactory, IWorkspaceService svc, ILogFactory logFactory):base(svc, logFactory) {
			_codeGenFactory = codeGenFactory;
			Title = "Code Generators";
		}

		public ObservableCollection<CodeGenerator> Items { get; } = new ObservableCollection<CodeGenerator>();

		public void Load() {
			//register whatever is in the settings file
			_codeGenFactory.Register();

			Items.Clear();
			foreach (var item in _codeGenFactory.Registrations) {
				Type type = item.GetType();
				Items.Add(new CodeGenerator(item));
			}
		}

		public RelayCommand RefreshCommand {
			get { return new RelayCommand(args => Load()); }
		}

		public RelayCommand NewCompositeCommand {
			get { return new RelayCommand(args => NewComposite()); }
		}
		void NewComposite() {
			WorkspaceService.Create<CompositeViewModel>();
		}


		CodeGenerator _codeGenerator;
		public CodeGenerator Selected {
			get { return _codeGenerator; }
			set {
				if (value != _codeGenerator) {
					_codeGenerator = value;
					RaisePropertyChanged(nameof(Selected));
				}
			}
		}

		public RelayCommand RunCommand {
			get { return new RelayCommand(args => Run(args)); }
		}
		void Run(object args) {
			if(args is CodeGenerator) { 
				WorkspaceService.Create<CodeGenerationViewModel>(vm => vm.Init(((CodeGenerator)args).Handle));
			}
		}
	}
}
