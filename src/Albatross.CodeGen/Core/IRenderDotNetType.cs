using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface IRenderDotNetType {
		StringBuilder Render(StringBuilder sb, Type type, bool nullable);
    }
}
