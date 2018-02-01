using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGenerator<T, O> {
		StringBuilder Build(StringBuilder sb, T t, O option, ICodeGeneratorFactory factory);
		IEnumerable<ICodeGenerator<T, O>> Children { get; set; }
	}
}
