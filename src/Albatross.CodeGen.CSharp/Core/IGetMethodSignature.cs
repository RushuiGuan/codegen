using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IGetMethodSignature {
		string Get(Method method);
	}
}
