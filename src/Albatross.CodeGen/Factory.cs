using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
    public class Factory<T>: IFactory<T>    {
		HashSet<T> set = new HashSet<T>();
		object sync = new object();

		public void Clear() {
			lock (sync) {
				set.Clear();
			}
		}

		public IEnumerable<T> List() {
			return set;
		}

		public void Register(T t) {
			lock (sync) {
				set.Add(t);
			}
		}
	}
}
