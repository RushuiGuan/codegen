using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell.ViewModel {
	[ListValue(typeof(SourceType))]
	public class SourceTypeList : ViewModelBase, IListValues<SourceType> {
		public SourceTypeList(SourceTypeFactory factory) {
			_items = factory.Get();
		}

		IEnumerable<SourceType> _items;
		public IEnumerable<SourceType> Items {
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
