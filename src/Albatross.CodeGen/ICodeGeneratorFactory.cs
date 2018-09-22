using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGeneratorFactory {
		CodeGenerator Get(string name);
		ICodeGenerator Create(string name);
		IEnumerable<CodeGenerator> Registrations { get; }
	}
}
