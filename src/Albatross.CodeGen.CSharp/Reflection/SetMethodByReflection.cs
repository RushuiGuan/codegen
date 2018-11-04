using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Reflection {
	public class SetMethodByReflection {
		GetParameterByReflection getParameterByReflection;
		GetTypeByReflection getTypeByReflection;

		public SetMethodByReflection(GetParameterByReflection getParameterByReflection, GetTypeByReflection getTypeByReflection) {
			this.getParameterByReflection = getParameterByReflection;
			this.getTypeByReflection = getTypeByReflection;
		}

		public Method Set(Method method, MethodInfo info) {
			method.Name = info.Name;
			method.Variables = from item in info.GetParameters() select getParameterByReflection.Get(item);
			method.ReturnType = getTypeByReflection.Get(info.ReturnType);
			method.Static = info.IsStatic;
			method.Virtual = info.IsVirtual;
			method.AccessModifier = info.IsPublic ? AccessModifier.Public : info.IsPrivate ? AccessModifier.Private : AccessModifier.Protected;
			return method;
		}
	}
}
