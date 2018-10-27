using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Reflection {
	public class GetMethodByReflection: IGetMethodByReflection {
		public Method Get(MethodInfo info) {
			return new Method {
				Name = info.Name,

			};
		}
	}
}
