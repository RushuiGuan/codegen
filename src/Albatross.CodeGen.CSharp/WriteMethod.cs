using Albatross.CodeGen.CSharp.Core;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp  {
	public class WriteMethod : IWriteObject<Method> {
		IWriteObject<AccessModifier> writeAccessModifier;
		IWriteObject<Variable> writeParam;
		IWriteObject<DotNetType> writeType;

		public WriteMethod(IWriteObject<AccessModifier> writeAccessModifier, IWriteObject<Variable> writeParam, IWriteObject<DotNetType> writeType) {
			this.writeAccessModifier = writeAccessModifier;
			this.writeParam = writeParam;
			this.writeType = writeType;
		}

		public string Write(Method t) {
			StringBuilder sb = new StringBuilder();
			sb.Write(writeAccessModifier, t.AccessModifier).Space();
			if (t.Static) { sb.Static(); }
			sb.Write(writeType, t.ReturnType).Space();

			sb.Append(t.Name).OpenParenthesis();
			if (t.Variables?.Count() > 0) {
				foreach (var param in t.Variables) {
					sb.Write(writeParam, param);
					sb.Comma().Space();
				}
				sb.TrimTrailingComma();
			}
			sb.CloseParenthesis();

			using(var scopedWriter = new WriteCSharpScopedObject(sb)) {
				scopedWriter.BeginScope();
				scopedWriter.Content.Append(t.Body);
			}
			return sb.ToString();
		}
	}
}