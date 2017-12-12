using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core
{
    public interface ICodeGenerator    {
		string Name { get; }
		string Category { get; }
		string Description { get; }
		StringBuilder Build(StringBuilder sb);
    }
}
