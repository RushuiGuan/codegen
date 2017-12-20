using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ICodeGeneratorFactory {
		ICodeGenerator<T> Get<T>(string name);
		ICodeGenerator Get(Type type, string name);
		IEnumerable<ICodeGenerator> Registrations { get; }
	}
}
