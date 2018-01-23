using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public interface ISaveFile<T> {
		void Save(T t);
	}
}
