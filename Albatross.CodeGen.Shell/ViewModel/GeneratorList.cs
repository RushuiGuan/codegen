using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.Shell.ViewModel {
	[ListValue(typeof(CodeGenerator))]
	public class GeneratorList : ViewModelBase, IListValues<CodeGenerator> {
		public GeneratorList(IConfigurableCodeGenFactory factory) {
			factory.Register();
			Items = from handle in factory.Registrations select new CodeGenerator(handle);
		}

		IEnumerable<CodeGenerator> _items;
		public IEnumerable<CodeGenerator> Items {
			get { return _items; }
			set {
				if (_items != value) {
					_items = value;
					RaisePropertyChanged(nameof(Items));
				}
			}
		}
	}
}
