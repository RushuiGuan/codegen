using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class GetMethodSignature : IGetMethodSignature {
		IWriteObject<DotNetType> getDotNetType;

		public GetMethodSignature(IWriteObject<DotNetType> writeDotNetType) {
			this.getDotNetType = writeDotNetType;
		}

		public string Get(Method method) {
			StringBuilder sb = new StringBuilder();
			sb.Append(method.Name).OpenParenthesis();
			if(method.Parameters?.Count()> 0) {
				foreach(var item in method.Parameters) {
					sb.Write(getDotNetType, item.Type).Space().Append(item.Name).Comma();
				}
				sb.TrimTrailingComma();
			}
			sb.CloseParenthesis();
			return sb.ToString();
		}
	}
}
