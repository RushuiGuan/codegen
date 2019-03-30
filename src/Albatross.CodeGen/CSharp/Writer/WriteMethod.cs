using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Writer
{
    public class WriteMethod : CodeGeneratorBase<Method> {
		ICodeGenerator<AccessModifier> writeAccessModifier;
		ICodeGenerator<Variable> writeParam;
		ICodeGenerator<DotNetType> writeType;

		public WriteMethod(ICodeGenerator<AccessModifier> writeAccessModifier, ICodeGenerator<Variable> writeParam, ICodeGenerator<DotNetType> writeType) {
			this.writeAccessModifier = writeAccessModifier;
			this.writeParam = writeParam;
			this.writeType = writeType;
		}

        public override void Run(TextWriter writer, Method t) {
			writer.Run(writeAccessModifier, t.AccessModifier).Space();
			if (t.Static) { writer.Static(); }
			writer.Run(writeType, t.ReturnType).Space();

			writer.Append(t.Name).OpenParenthesis();
			if (t.Variables?.Count() > 0) {
				foreach (var param in t.Variables) {
					writer.Run(writeParam, param);
					writer.Comma().Space();
				}
			}
			writer.CloseParenthesis();

            using (var scope = writer.BeginScope()){
				scope.Writer.Append(t.Body);
			}
		}
	}
}