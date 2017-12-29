using System.Reflection;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Tool.View;
using Albatross.CodeGen.Tool.ViewModel;
using Albatross.Logging.Core;
using SimpleInjector;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;

namespace Albatross.CodeGen.Tool {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IGetWorkspaceTabControl {
		ILog log;

		public MainWindow() {
			InitializeComponent();

			//configure container
			Container container = new ConfigContainer().Run();
			container.RegisterSingleton<IWorkspaceService, ShellViewModel>();
			container.RegisterSingleton<IGetWorkspaceTabControl>(this);
			container.Verify();

			//configure logging
			ILogFactory logFactory = container.GetInstance<ILogFactory>();
			logFactory.Init();
			log = logFactory.Get(this);
			log.Info("Contanier Initialized");

			//register views
			IViewLocator viewLocator = container.GetInstance<IViewLocator>();
			RegisterViews(viewLocator);
			this.Resources.Add(DefaultDataTemplateSelector.ViewLocatorKey, viewLocator);

			//configure automapper
			AutoMapper.Mapper.Initialize(ConfigAutoMapper);

			//setup nav
			Nav = container.GetInstance<NavigationViewModel>();
			
			//setup workspace service
			ShellViewModel = container.GetInstance<IWorkspaceService>();
			DataContext = this;
		}

		public void RegisterViews(IViewLocator locator) {
			foreach (Type type in this.GetType().Assembly.GetTypes()) {
				ViewUsageAttribute attrib = type.GetCustomAttribute<ViewUsageAttribute>();
				if (attrib != null) {
					locator.Register(type, attrib.ViewType);
				}
			}
			locator.Register<Table, TableInputView>()
				.Register<StoredProcedure, StoredProcedureInputView>()
				.Register<Server, ServerInputView>();
		}

		public void ConfigAutoMapper(IMapperConfigurationExpression cfg) {
			cfg.CreateMap<Composite, CompositeViewModel>().ReverseMap();
		}




		public IWorkspaceService ShellViewModel { get; private set; }
		public TabControl Get() { return tabs; }
		public NavigationViewModel Nav { get; private set; }
	}
}
