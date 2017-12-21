using Albatross.CodeGen.Shell.ViewModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Albatross.CodeGen.Shell {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window , IWorkspaceService{
		IObjectFactory factory;
		public MainWindow() {
			InitializeComponent();
			DataContext = this;
			Container container = new ConfigContainer().Run();
			container.RegisterSingleton<IWorkspaceService>(this);
			container.Verify();
			factory = container.GetInstance<IObjectFactory>();
		}

		public ObservableCollection<WorkspaceViewModel> Items { get; } = new ObservableCollection<WorkspaceViewModel>();

		public void Create<T>(Action<T> action) where T: WorkspaceViewModel {
			T t = factory.Create<T>();
			Items.Add(t);
			action?.Invoke(t);
			t.IsSelected = true;
		}

		public void CloseWorkspace(WorkspaceViewModel vm) {
			for (int i = 0; i < Items.Count; i++) {
				if (Items[i] == vm) {
					Items.RemoveAt(i);
					break;
				}
			}
		}


		public RelayCommand CodeGeneratorsCommand {
			get {
				return new RelayCommand(args => Create<CodeGeneratorCollectionViewModel>(vm=> vm.Load()));
			}
		}

	}
}
