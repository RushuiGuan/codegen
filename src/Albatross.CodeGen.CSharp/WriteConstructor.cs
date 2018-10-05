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
			sb.Write(writeAccessModifier, t.AccessModifier).Space();
			if (t.Static) { sb.Static(); }
			sb.Append(t.Name).OpenParenthesis();
			if (t.Parameters?.Count() > 0) {
				foreach (var param in t.Parameters) {
					if (param != t.Parameters.First()) {
						sb.Comma().Space();
					}
					sb.Write(writeParam, param);
				}
			}
			sb.CloseParenthesis();

			if(t.ChainedConstructor != null && t.ChainedConstructor.Parameters?.Count() > 0) {
				sb.Append(" : ").Append(t.ChainedConstructor.Name).OpenParenthesis();
				bool first = true;
				foreach(var item in t.ChainedConstructor.Parameters) {
					if (first) {
						sb.Append(item.Name);
						first = false;
					} else {
						sb.Comma().Space().Append(item.Name);
					}
				}
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