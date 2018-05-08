using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface IRunCodeGenerator {
		IEnumerable<object> Run(CodeGenerator gen, StringBuilder sb, IDictionary<string, string> customCode, object source, object option);
	}
}
