using Albatross.CodeGen.Database;
using Albatross.CodeGen.Shell.View;
using Albatross.CodeGen.Shell.ViewModel;
using Albatross.Logging.Core;
using SimpleInjector;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Albatross.CodeGen.Shell {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window , IWorkspaceService{
		IObjectFactory factory;
		ILog log;
		IViewLocator viewLocator;

		public MainWindow() {
			InitializeComponent();
			DataContext = this;
			Container container = new ConfigContainer().Run();
			container.RegisterSingleton<IWorkspaceService>(this);
			container.Verify();

			ILogFactory logFactory = container.GetInstance<ILogFactory>();
			logFactory.Init();
			log = logFactory.Get(this);
			log.Info("Contanier Initialized");
			factory = container.GetInstance<IObjectFactory>();
			viewLocator = container.GetInstance<IViewLocator>();
			RegisterViews(viewLocator);
			this.Resources.Add(DefaultDataTemplateSelector.ViewLocatorKey, viewLocator);
		}

		public const string ViewLocatorPropertyName = "ViewLocator";
		public static readonly DependencyProperty ViewLocatorProperty = DependencyProperty.Register(ViewLocatorPropertyName, typeof(IViewLocator), typeof(MainWindow), new PropertyMetadata());
		public IViewLocator ViewLocator {
			get { return (IViewLocator)GetValue(ViewLocatorProperty); }
			set { SetValue(ViewLocatorProperty, value); }
		}

		public void RegisterViews(IViewLocator locator) {
			locator.Register<CodeGeneratorCollectionViewModel, CodeGeneratorCollectionView>()
				.Register<CompositeDetailViewModel, CompositeDetailView>()
				.Register<Table, TableInputView>()
				.Register<StoredProcedure, StoredProcedureInputView>()
				.Register<Server, ServerInputView>()
				.Register<CodeGenerationViewModel, CodeGenerationView>();
		}

		public void Create<T>(Action<T> action) where T: WorkspaceViewModel {
			T t = factory.Create<T>();
			Type viewType = viewLocator.GetView(typeof(T));
			TabItem tab = new TabItem { Content = factory.Create(viewType), DataContext = t, };
			tabs.Items.Add(tab);
			action?.Invoke(t);
			t.IsSelected = true;
			log.Info("Created:" + t.GetHashCode());
		}

		public void CloseWorkspace(WorkspaceViewModel vm) {
			log.Info("Closing:" + vm.GetHashCode());
			for (int i = 0; i < tabs.Items.Count; i++) {
				if (((TabItem)tabs.Items[i]).DataContext == vm) {
					tabs.Items.RemoveAt(i);
					break;
				}
			}
		}

		public void Alert(string msg, string caption) {
			MessageBox.Show(msg, caption);
		}

		public bool Confirm(string msg, string caption) {
			return MessageBox.Show(msg, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
		}

		public RelayCommand CodeGeneratorsCommand { get { return new RelayCommand(args => Create<CodeGeneratorCollectionViewModel>(vm => vm.Load())); } }
	}
}
