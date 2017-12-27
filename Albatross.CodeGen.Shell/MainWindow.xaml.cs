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
	public partial class MainWindow : Window, IGetWorkspaceTabControl {
		ILog log;

		public MainWindow() {
			InitializeComponent();
			DataContext = this;
			Container container = new ConfigContainer().Run();
			container.RegisterSingleton<IWorkspaceService, ShellViewModel>();
			container.RegisterSingleton<IGetWorkspaceTabControl>(this);
			container.Verify();

			ILogFactory logFactory = container.GetInstance<ILogFactory>();
			logFactory.Init();
			log = logFactory.Get(this);
			log.Info("Contanier Initialized");

			IViewLocator viewLocator = container.GetInstance<IViewLocator>();
			RegisterViews(viewLocator);
			this.Resources.Add(DefaultDataTemplateSelector.ViewLocatorKey, viewLocator);
			ShellViewModel = container.GetInstance<IWorkspaceService>();
			Nav = container.GetInstance<NavigationViewModel>();
			DataContext = this;
		}

		public void RegisterViews(IViewLocator locator) {
			locator.Register<CodeGeneratorCollectionViewModel, CodeGeneratorCollectionView>()
				.Register<CompositeDetailViewModel, CompositeDetailView>()
				.Register<Table, TableInputView>()
				.Register<StoredProcedure, StoredProcedureInputView>()
				.Register<Server, ServerInputView>()
				.Register<CodeGenerationViewModel, CodeGenerationView>()
				.Register<NavigationViewModel, NavigationView>()
				.Register<CompositeCollectionViewModel, CompositeCollectionView>();
		}

		public IWorkspaceService ShellViewModel { get; private set; }
		public TabControl Get() { return tabs; }
		public NavigationViewModel Nav { get; private set; }
	}
}
