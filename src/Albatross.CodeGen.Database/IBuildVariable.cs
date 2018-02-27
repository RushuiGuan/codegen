using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database
{
    public interface IBuildVariable {
		StringBuilder Build(StringBuilder sb, Variable variable);
    }
}
