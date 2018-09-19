using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
    public interface INode {
		bool IsLeaf { get; }
		string Name { get; }
    }
}
