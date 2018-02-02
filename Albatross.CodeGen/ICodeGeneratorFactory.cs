using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGeneratorFactory {
		ICodeGenerator<T, O> Create<T, O>(string name);
		object Create(Type sourceType, string name);
		CodeGenerator GetMetadata(Type sourceType, string name);
		IEnumerable<CodeGenerator> Registrations { get; }
	}
}
