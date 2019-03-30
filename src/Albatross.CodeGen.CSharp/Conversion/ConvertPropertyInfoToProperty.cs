using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertPropertyInfoToProperty : IConvertObject<PropertyInfo, Property> {

        IConvertObject<Type, DotNetType> convertToDotNetType;

        public ConvertPropertyInfoToProperty(IConvertObject<Type, DotNetType> convertToDotNetType) {
            this.convertToDotNetType = convertToDotNetType;
		}

        public Property Convert(PropertyInfo from)
        {
            return new Property
            {
                Name = from.Name,
                Type = convertToDotNetType.Convert(from.PropertyType),
                CanWrite = from.CanWrite,
                CanRead = from.CanRead,
            };
        }
    }
}
