using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp.Reflection {
	public class GetTypeByReflection {
		public DotNetType Get(Type type) {
			var result = new DotNetType(type.FullName) {
				 IsGeneric =type.IsGenericType,
			};
			if (type.IsGenericType) {
				result.GenericTypes = from item in type.GetGenericArguments() select Get(item);
			} else {
				result.GenericTypes = new DotNetType[0];
			}
			return result;
		}
	}
}
