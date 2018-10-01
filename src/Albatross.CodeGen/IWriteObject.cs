using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public interface IWriteObject<T> {
		string Write(T t);
	}
}
