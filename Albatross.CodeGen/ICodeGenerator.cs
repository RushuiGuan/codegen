using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGenerator<in T, in O> {
		StringBuilder Build(StringBuilder sb, T source, O option, ICodeGeneratorFactory factory, out IEnumerable<object> used);
		event Func<StringBuilder, ICodeGeneratorFactory, IEnumerable<object>> Yield;
	}
}
