using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteParameter : IWriteObject<Variable> {
		IWriteObject<DotNetType> writeType;

		public WriteParameter(IWriteObject<DotNetType> writeType) {
			this.writeType = writeType;
		}

		public string Write(Variable t) {
			StringBuilder sb = new StringBuilder();


			if(t.Modifier == ParameterModifier.Out) {
				sb.Append("out ");
			}else if(t.Modifier == ParameterModifier.Ref) {
				sb.Append("ref ");
			}
			sb.Write(writeType, t.Type).Space().Append("@").Append(t.Name);
			return sb.ToString();
		}
	}
}
