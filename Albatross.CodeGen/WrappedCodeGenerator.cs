using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
	public class WrappedCodeGenerator<T, O,T1, O1> : ICodeGenerator<T, O> where T:T1  where O:O1 {
		ICodeGenerator<T1, O1> baseGen;

		public WrappedCodeGenerator(ICodeGenerator<T1, O1> baseGen) {
			this.baseGen = baseGen;
		}

		public event Func<StringBuilder, ICodeGeneratorFactory, IEnumerable<object>> Yield { add { } remove { } }

		public StringBuilder Build(StringBuilder sb, T source, O option, ICodeGeneratorFactory factory, out IEnumerable<object> used) {
			baseGen.Build(sb, source, option, factory, out used);
			return sb;
		}
	}
}
