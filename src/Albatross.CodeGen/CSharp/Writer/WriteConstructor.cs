using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System.IO;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteConstructor : CodeGeneratorBase<Constructor> {
		ICodeGenerator<AccessModifier> writeAccessModifier;
		ICodeGenerator<Variable> writeParam;

		public WriteConstructor(ICodeGenerator<AccessModifier> writeAccessModifier, ICodeGenerator<Variable> writeParam) {
			this.writeAccessModifier = writeAccessModifier;
			this.writeParam = writeParam;
		}


		public override void Run(TextWriter writer, Constructor t) {
			if (t.Static) {
				writer.Static();
			} else {
				writer.Run(writeAccessModifier, t.AccessModifier).Space();
			}
			writer.Append(t.Name).OpenParenthesis();
			if (t.Variables?.Count() > 0) {
				foreach (var param in t.Variables) {
					writer.Run(writeParam, param);
					writer.Comma().Space();
				}
			}
			writer.CloseParenthesis();

			if(t.BaseConstructor?.Variables?.Count() > 0) {
				writer.Append(" : ").Append(t.BaseConstructor.Name).OpenParenthesis();
				foreach (var item in t.BaseConstructor.Variables) {
					writer.Append("@").Append(item.Name);
					writer.Comma().Space();
				}
				writer.CloseParenthesis();
			}

			using(var scope= writer.BeginScope()) {
                scope.Writer.Append(t.Body);
			}
		}
	}
}