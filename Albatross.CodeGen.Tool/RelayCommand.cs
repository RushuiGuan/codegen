using System;
using System.Windows.Input;

namespace Albatross.CodeGen.Tool {
	public class RelayCommand : ICommand {

		Action<object> _execute;
		Func<object, bool> _canExecute;


		public RelayCommand(Action<object> execute) : this(execute, null) { }
		public RelayCommand(Action<object> execute, Func<object, bool> canExecute) {
			if (execute == null) {
				throw new ArgumentNullException("execute");
			}
			_execute = execute;
			if (canExecute != null) { _canExecute = canExecute; }
		}

		public event EventHandler CanExecuteChanged {
			add { if (_canExecute != null) { CommandManager.RequerySuggested += value; } }
			remove { if (_canExecute != null) { CommandManager.RequerySuggested -= value; } }
		}

		public void RaiseCanExecuteChanged() {
			CommandManager.InvalidateRequerySuggested();
		}

		public bool CanExecute(object parameter) {
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object parameter) {
			if (CanExecute(parameter) && _execute != null) {
				_execute(parameter);
			}
		}
	}

	public class RelayCommand<T> : RelayCommand {
		public RelayCommand(Action<T> execute) : this(execute, null) { }
		public RelayCommand(Action<T> execute, Func<T, bool> canExecute) : base(Convert(execute), Convert(canExecute)) {
		}

		static Action<object> Convert(Action<T> action) {
			if (action == null) {
				return null;
			} else {
				return args => {
					if (args == null) {
						action(default(T));
					} else {
						action((T)args);
					}
				};
			}
		}
		static Func<object, bool> Convert(Func<T, bool> func) {
			if (func == null) {
				return null;
			} else {
				return args => {
					if (args == null) {
						return func(default(T));
					} else {
						return func((T)args);
					}
				};
			}
		}
	}
}
