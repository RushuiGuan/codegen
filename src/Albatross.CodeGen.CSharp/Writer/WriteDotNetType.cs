using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteDotNetType: IWriteObject<DotNetType> {
		public string Write(DotNetType type) {
			StringBuilder sb = new StringBuilder();
			sb.Append(type.Name);
			if (type.IsGeneric && type.GenericTypes?.Count() > 0) {
				sb.OpenAngleBracket();
				bool first = true;
				foreach(var genericType in type.GenericTypes) {
					if (!first) {
						sb.Comma().Space();
					} else {
						first = false;
					}
					sb.Append(this.Write(genericType));
				}
				sb.CloseAngleBracket();
			}
			 return sb.ToString();
		}
	}
}
