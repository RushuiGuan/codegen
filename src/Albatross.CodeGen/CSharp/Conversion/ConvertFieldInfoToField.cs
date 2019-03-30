using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertFieldInfoToField : IConvertObject<FieldInfo, Field> {
        IConvertObject<Type, DotNetType> convertToDotNetType;
        public ConvertFieldInfoToField(IConvertObject<Type, DotNetType> convertToDotNetType) {
            this.convertToDotNetType = convertToDotNetType;
        }

        public Field Convert(FieldInfo from)
        {
            return new Field
            {
                Name = from.Name,
                Type = convertToDotNetType.Convert(from.FieldType),
                ReadOnly = from.IsInitOnly,
            };
        }

        object IConvertObject<FieldInfo>.Convert(FieldInfo from)
        {
            return this.Convert(from);
        }
    }
}
