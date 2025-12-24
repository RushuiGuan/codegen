using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record TypeScriptCodeBlock : CodeBlock {
		public TypeScriptCodeBlock() {
			Prefix = "{";
			PostFix = "}";
		}
		protected override void WriteItem(TextWriter writer, IExpression item) {
			writer.Code(item);
			if (item is not ICodeBlock) {
				writer.Semicolon();
			}
		}
	}
}