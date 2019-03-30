using Albatross.CodeGen.CSharp.Model;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteField : IWriteObject<Field> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<DotNetType> writeType;

		public WriteField(IWriteObject<AccessModifier> renderAccessModifier, IWriteObject<DotNetType> renderType) {
			this.writeAccessModifier = renderAccessModifier;
			this.writeType = renderType;
		}

		public string Write(Field field) {
			StringBuilder sb = new StringBuilder();
			sb.Write(writeAccessModifier, field.Modifier).Space();
			if (field.Static) { sb.Static(); }
			if (field.ReadOnly) { sb.ReadOnly(); }
			sb.Write(writeType, field.Type).Space().Append(field.Name);

			if(field.Assignment.Length > 0) {
				sb.Append(" = ").Append(field.Assignment);
			}
			sb.Semicolon();
			return sb.ToString();
		}
	}
}
