using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System.Reflection;

namespace Albatross.CodeGen.CSharp.Conversion {
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

        object IConvertObject<ParameterInfo>.Convert(ParameterInfo from)
        {
            return this.Convert(from);
        }
    }
}
