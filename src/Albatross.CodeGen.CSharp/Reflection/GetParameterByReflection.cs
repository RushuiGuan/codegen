using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Reflection {
	public class GetParameterByReflection {
			GetTypeByReflection getTypeByReflection;
		public GetParameterByReflection(GetTypeByReflection getTypeByReflection) {
			this.getTypeByReflection = getTypeByReflection;
		}

		public Variable Get(ParameterInfo info) {
			return new Variable {
				Name = info.Name,
				Type = getTypeByReflection.Get(info.ParameterType),
				Modifier = info.IsOut ? Core.ParameterModifier.Out : info.IsIn ? Core.ParameterModifier.In : Core.ParameterModifier.Ref,
			};
		}
	}
}
