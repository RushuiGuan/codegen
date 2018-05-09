using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core {
	public interface ICustomCodeSectionStrategy {
		ICustomCodeSection Get(string target);
	}
}
