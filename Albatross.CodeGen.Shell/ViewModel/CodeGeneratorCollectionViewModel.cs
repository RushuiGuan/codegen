using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CodeGeneratorCollectionViewModel : WorkspaceViewModel {
		IConfigurableCodeGenFactory _codeGenFactory;

		public CodeGeneratorCollectionViewModel(IConfigurableCodeGenFactory codeGenFactory, IWorkspaceService svc):base(svc) {
			_codeGenFactory = codeGenFactory;
			Title = "Code Generators";
		}

		public ObservableCollection<CodeGenerator> Items { get; } = new ObservableCollection<CodeGenerator>();

		public void Load() {
			_codeGenFactory.Clear();
			//register whatever is in the settings file
			_codeGenFactory.Register();

			//register bulit in generators
			_codeGenFactory.RegisterComposites(Albatross.CodeGen.SqlServer.Pack.Composites);
			_codeGenFactory.RegisterAdditional(typeof(Albatross.CodeGen.SqlServer.Pack).Assembly);

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
			WorkspaceService.Create<CompositeDetailViewModel>(args => args.Load(null));
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
			get { return new RelayCommand(args => Run()); }
		}
		void Run() {
			if (Selected != null) {
				WorkspaceService.Create<CodeGenerationViewModel>(vm => vm.Init(Selected.Handle));
			}
		}
	}
}
