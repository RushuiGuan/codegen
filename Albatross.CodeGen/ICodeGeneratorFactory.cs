using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGeneratorFactory {
		ICodeGenerator<T, O> Get<T, O>(string name);
		ICodeGenerator Get(Type type, string name);
		IEnumerable<CodeGeneratorAttribute> Registrations { get; }
	}
}
