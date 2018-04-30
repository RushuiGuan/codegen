using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface IRenderDotNetType {
		void Render(StringBuilder sb, Type type);
    }
}
