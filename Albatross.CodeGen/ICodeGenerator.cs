using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGenerator {
		string Name { get; }
		string Category { get; }
		string Description { get; }
		string Target { get; }

		StringBuilder Build(StringBuilder sb, object t, ICodeGeneratorFactory factory);
	}

	public interface ICodeGenerator<T> : ICodeGenerator {
		StringBuilder Build(StringBuilder sb, T t, ICodeGeneratorFactory factory);
	}
}
