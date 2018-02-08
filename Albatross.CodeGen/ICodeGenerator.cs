using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGenerator<in T, in O> {
		void Configure(object data);
		IEnumerable<object> Build(StringBuilder sb, T source, O option);
		event Func<StringBuilder, IEnumerable<object>> Yield;
	}
}
