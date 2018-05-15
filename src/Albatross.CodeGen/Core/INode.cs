using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
	public interface INode {
		string Name { get; }

		object Source { get; }
		ICodeGeneratorOption Option { get; }
		ICodeGenerator CodeGenerator { get; }
	}
}
