using System.Collections.Generic;
using System.ComponentModel;

namespace Albatross.CodeGen.Tool {
	public interface IListValues<T> : INotifyPropertyChanged{
		IEnumerable<T> Items { get; }
	}
}
