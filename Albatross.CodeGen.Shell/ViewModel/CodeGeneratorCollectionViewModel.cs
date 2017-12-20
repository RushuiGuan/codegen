using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CodeGeneratorCollectionViewModel : WorkspaceViewModel {
		IConfigurableCodeGenFactory _codeGenFactory;
		IWorkspaceService _workspaceService;

		public CodeGeneratorCollectionViewModel(IConfigurableCodeGenFactory codeGenFactory, IWorkspaceService svc) {
			_codeGenFactory = codeGenFactory;
			_workspaceService = svc;
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
				CodeGenerator gen = new CodeGenerator {
					Name = item.Name,
					Category = item.Category,
					Description = item.Description,
					Target = item.Target,
					Type = type.FullName,
					Location = type.Assembly.Location,
					UserDefined = false,
				};
				Items.Add(gen);
			}
		}

		public RelayCommand RefreshCommand {
			get { return new RelayCommand(args => Load()); }
		}

		public RelayCommand NewCompositeCommand {
			get { return new RelayCommand(args => NewComposite()); }
		}
		void NewComposite() {
			_workspaceService.Create<CompositeDetailViewModel>(args => args.Load(null));
		}

		public RelayCommand EditCommand {
			get { return new RelayCommand(args => Edit()); }
		}
		void Edit() {
		}
	}
}
