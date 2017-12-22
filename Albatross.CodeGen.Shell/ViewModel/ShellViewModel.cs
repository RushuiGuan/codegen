using Albatross.Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class ShellViewModel : ViewModelBase, IWorkspaceService {
		IObjectFactory factory;
		IViewLocator viewLocator;
		ILog log;
		TabControl tabs;


		TabItem _current;
		public TabItem Current {
			get { return _current; }
			set {
				if (_current != value) {
					_current = value;
					RaisePropertyChanged(nameof(Current));
				}
			}
		}


		public ShellViewModel(IObjectFactory factory, IViewLocator viewLocator, ILogFactory logFactory, IGetWorkspaceTabControl getTabControl) {
			this.factory = factory;
			this.viewLocator = viewLocator;
			tabs = getTabControl.Get();
			log = logFactory.Get(this);
		}

		public void Create<T>(Action<T> action = null) where T : WorkspaceViewModel {
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
	}
}
