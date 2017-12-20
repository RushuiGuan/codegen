using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class CodeGeneratorCollectionViewModel : ViewModelBase {



		public ObservableCollection<CodeGenerator> Items { get; } = new ObservableCollection<CodeGenerator>();

		public void Load() {

		}
	}
}
