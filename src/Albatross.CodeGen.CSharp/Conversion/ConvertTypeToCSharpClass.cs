using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertTypeToCSharpClass : IConvertObject<Type, Class> {

        IConvertObject<PropertyInfo, Property> convertProperty;
        IConvertObject<FieldInfo, Field> convertField;
        public ConvertTypeToCSharpClass(IConvertObject<PropertyInfo, Property> convertProperty, IConvertObject<FieldInfo, Field> convertField) {
            this.convertProperty = convertProperty;
            this.convertField = convertField;
        }
        public Class Convert(Type type)
        {
            var result = new Class(type.Name)
            {
                Namespace = type.Namespace,
            };
            if(type.IsPublic) { result.AccessModifier = AccessModifier.Public;  }
            result.Sealed = type.IsSealed;
            if (type.BaseType != null &&  type.BaseType != typeof(object))
            {
                result.BaseClass = Convert(type.BaseType);
            }
            result.Properties = from p in type.GetProperties() select convertProperty.Convert(p);
            result.Fields = from f in type.GetFields() select convertField.Convert(f);

            return result;
        }
	}
}
