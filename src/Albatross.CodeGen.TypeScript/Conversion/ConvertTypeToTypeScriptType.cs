using Albatross.CodeGen.TypeScript.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.TypeScript.Conversion
{
    public class ConvertTypeToTypeScriptType : IConvertObject<Type, TypeScriptType>
    {
        public TypeScriptType Convert(Type type)
        {
            if (type == typeof(string))
            {
                return TypeScriptType.String;
            }
            else if (type == typeof(float))
            {
                return TypeScriptType.Number;
            }
            else if (type == typeof(bool))
            {
                return TypeScriptType.Boolean;
            }
            else if (type == typeof(void))
            {
                return TypeScriptType.Void;
            }
            else if (type == typeof(object))
            {
                return TypeScriptType.Any;
            }
            else if (GetCollectionType(type, out Type elementType))
            {
                var result = this.Convert(elementType);
                result.IsArray = true;
                return result;
            }
            else
            {
                return new TypeScriptType(type.Name);
            }
        }

        public bool GetCollectionType(Type type, out Type elementType) {
            elementType = null;

            if (type == typeof(string))
            {
                return false;
            } else if (type == typeof(Array) || type.IsArray)
            {
                elementType = type.GetElementType();
                if (elementType == null)
                {
                    elementType = typeof(object);
                }
            }
            else if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>))) { 
                elementType = type.GetGenericArguments().First();
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                elementType = typeof(object);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
