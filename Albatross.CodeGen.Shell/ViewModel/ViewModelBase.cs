﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class ViewModelBase : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged(string name) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
