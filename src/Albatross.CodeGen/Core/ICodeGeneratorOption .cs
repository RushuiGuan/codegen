using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
	public interface ICodeGeneratorOption {
		bool AllowCustomCode { get; }
	}
}
