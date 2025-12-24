using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class IfExpression : CodeNode, ICodeBlock {
		public IfExpression() { }

		public required IExpression Condition { get; init; }
		public CodeBlock CodeBlock { get; } = new PythonCodeBlock();
		public CodeBlock ElseBlock { get; } = new PythonCodeBlock();

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode> {
			Condition, CodeBlock
		}.AddIfNotNull(ElseBlock);

		public override TextWriter Generate(TextWriter writer) {
			writer.Append("if ").OpenParenthesis().Code(Condition).CloseParenthesis().Code(CodeBlock);
			if (ElseBlock.Any()) {
				writer.Append(" else").Code(ElseBlock);
			}
			return writer;
		}
	}
}