using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Shell {
	public interface IConfigurableCodeGenFactory : ICodeGeneratorFactory {
		void Clear();
		void Register();
		void RegisterAdditional(Assembly asm);
		void RegisterComposites(IEnumerable<Composite> items);
	}
}
