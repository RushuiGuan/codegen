using Albatross.CodeGen.CSharp.Model;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteConstructor : IWriteObject<Constructor> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<Variable> writeParam;

		public WriteConstructor(IWriteObject<AccessModifier> writeAccessModifier, IWriteObject<Variable> writeParam) {
			this.writeAccessModifier = writeAccessModifier;
			this.writeParam = writeParam;
		}


		public string Write(Constructor t) {
			StringBuilder sb = new StringBuilder();
			if (t.Static) {
				sb.Static();
			} else {
				sb.Write(writeAccessModifier, t.AccessModifier).Space();
			}
			sb.Append(t.Name).OpenParenthesis();
			if (t.Variables?.Count() > 0) {
				foreach (var param in t.Variables) {
					sb.Write(writeParam, param);
					sb.Comma().Space();
				}
				sb.TrimTrailingComma();
			}
			sb.CloseParenthesis();

			if(t.BaseConstructor?.Variables?.Count() > 0) {
				sb.Append(" : ").Append(t.BaseConstructor.Name).OpenParenthesis();
				foreach (var item in t.BaseConstructor.Variables) {
					sb.Append("@").Append(item.Name);
					sb.Comma().Space();
				}
				sb.TrimTrailingComma();
				sb.CloseParenthesis();
			}

			using(var scopedWriter = new WriteCSharpScopedObject(sb)) {
				scopedWriter.BeginScope();
				scopedWriter.Content.Append(t.Body);
			}
			return sb.ToString();
		}
	}
}