using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IOverrideClassObject {
		Class Get(Class src, Class @override);
	}
}
