using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class CSharpCodeBlock : CodeBlock {
		public CSharpCodeBlock() {
			Prefix = "{";
			PostFix = "}";
		}
		public override TextWriter Generate(TextWriter writer) {
			if (nodes.Count == 0) {
				return writer.Append("{ }");
			} else {
				return base.Generate(writer);
			}
		}
		protected override void WriteItem(TextWriter writer, IExpression item) {
			writer.Code(item);
			if (item is not ICodeBlock) {
				writer.Semicolon();
			}
		}
	}
}