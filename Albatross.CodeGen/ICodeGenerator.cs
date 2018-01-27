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
		Type SourceType { get; }
		Type OptionType { get; }

		StringBuilder Build(StringBuilder sb, object t, object options, ICodeGeneratorFactory factory);
	}

	public interface ICodeGenerator<T, O> : ICodeGenerator {
		StringBuilder Build(StringBuilder sb, T t, O option, ICodeGeneratorFactory factory);
		IEnumerable<ICodeGenerator<T, O>> Children { get; set; }
	}
}
