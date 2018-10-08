using Albatross.CodeGen.CSharp.Core;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp  {
	public class WriteConstructor : IWriteObject<Constructor> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<Parameter> writeParam;

		public WriteConstructor(IWriteObject<AccessModifier> writeAccessModifier, IWriteObject<Parameter> writeParam) {
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
			if (t.Parameters?.Count() > 0) {
				foreach (var param in t.Parameters) {
					sb.Write(writeParam, param);
					sb.Comma().Space();
				}
				sb.TrimTrailingComma();
			}
			sb.CloseParenthesis();

			if(t.ChainedConstructor?.Parameters?.Count() > 0) {
				sb.Append(" : ").Append(t.ChainedConstructor.Name).OpenParenthesis();
				foreach (var item in t.ChainedConstructor.Parameters) {
					sb.Append(item.Name);
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