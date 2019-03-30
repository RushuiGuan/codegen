using Albatross.CodeGen.Core;
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
            if (type == typeof(string) || type == typeof(char))
            {
                return TypeScriptType.String;
            }
            else if (type == typeof(float) || type == typeof(decimal) || type == typeof(double)
                || type == typeof(byte) || type == typeof(sbyte) 
                || type == typeof(short) || type == typeof(int) || type == typeof(long)
                || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
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
            else if (type == typeof(DateTime))
            {
                return TypeScriptType.Date;
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
            }else if(GetNullableType(type, out Type targetType))
            {
                return this.Convert(targetType);
            }
            else
            {
                return new TypeScriptType(type.Name);
            }
        }

        public bool GetNullableType(Type nullableType, out Type result) {
            result = null;
            if (nullableType.IsGenericType && nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                result = nullableType.GetGenericArguments()[0];
                return true;
            }
            return false;
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
            else if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                elementType = type.GetGenericArguments().First();
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

        object IConvertObject<Type>.Convert(Type from)
        {
            return this.Convert(from);
        }
    }
}
