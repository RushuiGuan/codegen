using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion{
	public class ConvertParameterInfoToVariable: IConvertObject<ParameterInfo, Variable> {

        ConvertTypeToDotNetType convertType;

		public ConvertParameterInfoToVariable(ConvertTypeToDotNetType getTypeByReflection) {
			this.convertType = getTypeByReflection;
		}

		public Variable Convert(ParameterInfo info) {
			return new Variable {
				Name = info.Name,
				Type = convertType.Convert(info.ParameterType),
				Modifier = info.IsOut ? Model.ParameterModifier.Out : info.IsIn ? Model.ParameterModifier.In : Model.ParameterModifier.Ref,
			};
		}
	}
}
