using System.Collections.Generic;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record TypeScriptCodeBlock : CodeBlock {
		public TypeScriptCodeBlock() {
			Separator = ";";
		}
	}
}