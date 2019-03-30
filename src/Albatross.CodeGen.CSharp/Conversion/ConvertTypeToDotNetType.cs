using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertTypeToDotNetType: IConvertObject<Type, DotNetType> {
        public DotNetType Convert(Type type)
        {
            var result = new DotNetType(type.FullName)
            {
                IsGeneric = type.IsGenericType,
            };
            if (type.IsGenericType)
            {
                result.GenericTypes = from item in type.GetGenericArguments() select Convert(item);
            }
            else
            {
                result.GenericTypes = new DotNetType[0];
            }
            return result;
        }
	}
}
