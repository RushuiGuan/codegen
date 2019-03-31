using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System.Reflection;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertParameterInfoToParameter : IConvertObject<ParameterInfo, Parameter> {

        ConvertTypeToDotNetType convertType;

		public ConvertParameterInfoToParameter (ConvertTypeToDotNetType getTypeByReflection) {
			this.convertType = getTypeByReflection;
		}

		public Parameter Convert(ParameterInfo info) {
			return new Parameter {
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
