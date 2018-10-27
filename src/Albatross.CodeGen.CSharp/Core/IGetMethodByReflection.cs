using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IGetMethodByReflection {
		Method Get(MethodInfo methodInfo);
	}
}
