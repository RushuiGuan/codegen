using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertMethodInfoToMethod: IConvertObject<MethodInfo, Method> {
        ConvertParameterInfoToVariable convertToVariable;
		ConvertTypeToDotNetType convertToDotNetType;

		public ConvertMethodInfoToMethod(ConvertParameterInfoToVariable convertToVariable, ConvertTypeToDotNetType convertToDotNetType) {
			this.convertToVariable = convertToVariable;
			this.convertToDotNetType = convertToDotNetType;
		}

        public Method Convert(MethodInfo info)
        {
            Method method = new Method
            {
                Name = info.Name,
                Variables = from item in info.GetParameters() select convertToVariable.Convert(item),
                ReturnType = convertToDotNetType.Convert(info.ReturnType),
                Static = info.IsStatic,
                Virtual = info.IsVirtual,
                AccessModifier = info.IsPublic ? AccessModifier.Public : info.IsPrivate ? AccessModifier.Private : AccessModifier.Protected,
            };
            return method;
        }
	}
}
