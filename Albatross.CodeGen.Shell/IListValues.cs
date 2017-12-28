using System.Collections.Generic;
using System.ComponentModel;

namespace Albatross.CodeGen.Shell {
	public interface IListValues<T> : INotifyPropertyChanged{
		IEnumerable<T> Items { get; }
	}
}
