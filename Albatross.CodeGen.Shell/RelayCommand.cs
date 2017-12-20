using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Albatross.CodeGen.Shell {
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
}
