using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public interface IWriteScopedObject<T> : IDisposable {
		StringBuilder Content { get; }
		IWriteScopedObject<T> BeginScope(T t);
		IWriteScopedObject<T> BeginChildScope(T t);

		void EndScope();
	}
}
