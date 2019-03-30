using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertTypeToDotNetType: IConvertObject<Type, DotNetType> {
        public DotNetType Convert(Type type)
        {
			string name;
			bool isArray;
			bool isGenericType;
			IEnumerable<DotNetType> genericTypeArguments=null;

			isArray = type.IsArray;
			if (isArray) {
				type = type.GetElementType();
			}
		
			isGenericType = type.IsGenericType;
			if (isGenericType) {
				name = type.GetGenericTypeDefinition().FullName;
				name = name.Substring(0, name.LastIndexOf('`'));
				genericTypeArguments = (from item in type.GetGenericArguments() select Convert(item)).ToArray();
			} else {
				name = type.FullName;
			}
			return new DotNetType(name, isArray, isGenericType, genericTypeArguments);
        }

        object IConvertObject<Type>.Convert(Type from)
        {
            return this.Convert(from);
        }
    }
}
