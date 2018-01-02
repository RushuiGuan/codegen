using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface IConfigurableCodeGenFactory : ICodeGeneratorFactory {
		void Clear();
		void Register();
		void Register(Assembly asm);
		void Register(IEnumerable<Composite> items);
	}
}
