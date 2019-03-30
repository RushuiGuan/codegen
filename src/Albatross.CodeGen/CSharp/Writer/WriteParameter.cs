using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteParameter : CodeGeneratorBase<Variable> {
		ICodeGenerator<DotNetType> writeType;

		public WriteParameter(ICodeGenerator<DotNetType> writeType) {
			this.writeType = writeType;
		}

        public override void Run(TextWriter writer, Variable t)
        {
			if(t.Modifier == ParameterModifier.Out) {
				writer.Append("out ");
			}else if(t.Modifier == ParameterModifier.Ref) {
				writer.Append("ref ");
			}
			writer.Run(writeType, t.Type).Space().Append("@").Append(t.Name);
		}
	}
}
