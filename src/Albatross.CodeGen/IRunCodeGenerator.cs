using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
    public interface IRunCodeGenerator {
		IEnumerable<object> Run(CodeGenerator gen, StringBuilder sb, object source, object option);
	}
}
