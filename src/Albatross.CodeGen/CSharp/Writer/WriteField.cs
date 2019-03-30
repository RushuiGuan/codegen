using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteField : CodeGeneratorBase<Field> {
		ICodeGenerator<AccessModifier> writeAccessModifier;
		ICodeGenerator<DotNetType> writeType;

		public WriteField(ICodeGenerator<AccessModifier> renderAccessModifier, ICodeGenerator<DotNetType> renderType) {
			this.writeAccessModifier = renderAccessModifier;
			this.writeType = renderType;
		}

		public override void Run(TextWriter writer, Field field) {
            writer.Run(writeAccessModifier, field.Modifier).Space();
			if (field.Static) { writer.Static(); }
			if (field.ReadOnly) { writer.ReadOnly(); }
            writer.Run(writeType, field.Type).Space().Append(field.Name);

			if (field.Assignment.Length > 0) {
				writer.Append(" = ").Append(field.Assignment);
			}
			writer.Semicolon();
		}
	}
}
