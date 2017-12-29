using Albatross.Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Albatross.CodeGen.Tool.ViewModel {
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
					if (_current != null) {
						_current.IsSelected = false;
					}
					_current = value;
					if (_current != null) { _current.IsSelected = true; }
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

		bool TryFindWorkspace<T>(object id, out TabItem tab) {
			tab = null;
			if (id == null) {
				return false;
			} else {
				foreach (TabItem item in tabs.Items) {
					if(item.DataContext?.GetType() == typeof(T)) {
						WorkspaceViewModel vm = item.DataContext as WorkspaceViewModel;
						if (object.Equals(vm?.ID, id)) {
							tab = item;
							return true;
						}
					}
				}
			}
			return false;
		}

		public void Create<T>(Action<T> action = null, object id = null) where T : WorkspaceViewModel {
			TabItem tab;

			if (!TryFindWorkspace<T>(id, out tab)) {
				T t = factory.Create<T>();
				t.ID = id;
				Type viewType = viewLocator.GetView(typeof(T));
				tab = new TabItem { Content = factory.Create(viewType), DataContext = t, };
				tabs.Items.Add(tab);
				action?.Invoke(t);
				log.Info("Created:" + t.GetHashCode());
			}
			Current = tab;
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
