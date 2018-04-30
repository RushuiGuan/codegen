using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
    public interface INode {
		bool IsLeaf { get; }
		string Name { get; }
    }
}
