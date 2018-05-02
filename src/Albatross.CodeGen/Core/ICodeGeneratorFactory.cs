using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core {
	public interface ICodeGeneratorFactory {
		CodeGenerator Get(Type type, string name);
		ICodeGenerator<T, O> Create<T, O>(string name) where T:class where O:class;
		object Create(Type type, string name);
		IEnumerable<CodeGenerator> Registrations { get; }
	}
}
