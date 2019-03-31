using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Linq;
using System.Reflection;

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
				Static = from.GetAccessors().Any(args => args.IsStatic),
			};

			
        }

        object IConvertObject<PropertyInfo>.Convert(PropertyInfo from)
        {
            return this.Convert(from);
        }
    }
}
