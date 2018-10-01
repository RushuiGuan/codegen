using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public abstract class WriteScopedObject<T> : IWriteScopedObject<T> {
		protected StringBuilder Parent { get; private set; }
		public StringBuilder Content { get; } = new StringBuilder();

		public WriteScopedObject(StringBuilder parent) {
			Parent = parent;
		}

		public abstract IWriteScopedObject<T> BeginScope(T t);
		public abstract IWriteScopedObject<T> BeginChildScope(T t);

		public void Dispose() {
			Parent.Tabify(Content.ToString(), 1);
			Parent.AppendLine();
			EndScope();
		}

		public abstract void EndScope();
	}
}
