using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core {
	public interface ICodeGeneratorFactory {
		CodeGenerator Get(Type sourceType, string name);
		ICodeGenerator<T, O> Create<T, O>(string name);
		object Create(Type srcType, string name);
		IEnumerable<CodeGenerator> Registrations { get; }
	}
}
