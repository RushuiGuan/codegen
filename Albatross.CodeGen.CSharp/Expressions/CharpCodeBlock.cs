using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class CSharpCodeBlock : CodeBlock {
		public CSharpCodeBlock() {
			Separator = ";";
			Multiline = true;
		}
	}
}