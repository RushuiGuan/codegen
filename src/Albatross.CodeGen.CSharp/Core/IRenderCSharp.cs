using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IRenderCSharp<T> {
		StringBuilder Render(StringBuilder sb, T part);
	}
}
