using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
    public interface IFactory<T>
    {
		void Clear();
		IEnumerable<T> List();
		void Register(T t);
    }
}
